using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace GameExpress.Controls
{
    /// <summary>
    /// Editor für statische Objekte
    /// </summary>
    public class EditorPanel : Panel
    {
        /// <summary>
        /// Liefert oder setzt die Ansicht
        /// </summary>
        protected CanvasControl Content { get; set; }

        /// <summary>
        /// Liefert oder setzt das vertikale Lineal
        /// </summary>
        protected CanvasControl VerticalRuler { get; set; }

        /// <summary>
        /// Liefert oder setzt das horizontale Lineal
        /// </summary>
        protected CanvasControl HorizontalRuler { get; set; }

        /// <summary>
        /// Liefert oder setzt den ZoomSlider
        /// </summary>
        protected Slider ZoomSlider { get; set; }

        /// <summary>
        /// Liefert oder setzt den GridButton
        /// </summary>
        protected ToggleButton GridButton { get; set; }

        /// <summary>
        /// Liefert oder setzt den RulerButton
        /// </summary>
        protected ToggleButton RulerButton { get; set; }

        /// <summary>
        /// Liefert oder setzt den RulerButton
        /// </summary>
        protected CommandBar CommandBar { get; set; }

        /// <summary>
        /// Liefert oder setzt den ScrollViewer
        /// </summary>
        protected ScrollViewer ScrollViewer { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die derigistrierung benötigt wird
        /// </summary>
        private long GridVisibilityPropertyToken { get; set; }

        /// <summary>
        /// Liefert oder setzt das Device-Objekt
        /// </summary>
        private CompositionGraphicsDevice Device { get; set; }

        /// <summary>
        /// Liefert oder setzt das DrawingSurface-Objekt
        /// </summary>
        private CompositionDrawingSurface DrawingSurface { get; set; }

        /// <summary>
        /// Leifert oder setzt die DrawingSession
        /// </summary>
        private CanvasDrawingSession DrawingSession { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public EditorPanel()
        {
            DefaultStyleKey = typeof(EditorPanel);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Content = GetTemplateChild("Content") as CanvasControl;

            if (Content != null)
            {
                Content.CreateResources += OnCreateResources;
                Content.Draw += OnDrawContent;
            }

            VerticalRuler = GetTemplateChild("VerticalRuler") as CanvasControl;

            if (VerticalRuler != null)
            {
                VerticalRuler.Draw += OnDrawVerticalRuler;
            }

            HorizontalRuler = GetTemplateChild("HorizontalRuler") as CanvasControl;

            if (HorizontalRuler != null)
            {
                HorizontalRuler.Draw += OnDrawHorizontalRuler;
            }

            ZoomSlider = GetTemplateChild("ZoomSlider") as Slider;

            if (ZoomSlider != null)
            {
                ZoomSlider.ValueChanged += OnZoomValueChanged;
            }

            GridButton = GetTemplateChild("GridButton") as ToggleButton;

            if (GridButton != null)
            {
                GridButton.DataContext = this;
            }

            RulerButton = GetTemplateChild("RulerButton") as ToggleButton;

            if (RulerButton != null)
            {
                RulerButton.DataContext = this;
            }

            CommandBar = GetTemplateChild("CommandBar") as CommandBar;

            if (CommandBar != null)
            {
            }

            ScrollViewer = GetTemplateChild("ScrollViewer") as ScrollViewer;

            if (ScrollViewer != null)
            {
                ScrollViewer.ViewChanged += OnViewChanged;
                ScrollViewer.Loaded += OnScrollViewerLoaded;
            }

            // Eigenschaft GridVisibilityProperty hat sich geändert
            GridVisibilityPropertyToken = RegisterPropertyChangedCallback(GridVisibilityProperty, new DependencyPropertyChangedCallback((s, e) => 
            {
                // Neuzeichnen erforderlich
                Content?.Invalidate();
            }));

            // Eigenschaften des Items haben sich geändert
            if (Item != null)
            {
                Item.PropertyChanged += OnInvalidate;
            }
        }

        /// <summary>
        /// Tritt ein, wenn der Bildlauf oder der Zoom geändert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnViewChanged(object sender, ScrollViewerViewChangedEventArgs args)
        {
            Content.SetValue(Canvas.TopProperty, ScrollViewer.VerticalOffset);
            Content.SetValue(Canvas.LeftProperty, ScrollViewer.HorizontalOffset);

            Invalidate();
        }

        /// <summary>
        /// Zwingt das Control zum neuzeichnen
        /// </summary>
        public virtual void Invalidate()
        {
            //OnDrawContent(new CanvasDrawEventArgs(DrawingSession));
            Content.Invalidate();
            HorizontalRuler.Invalidate();
            VerticalRuler.Invalidate();
        }

        /// <summary>
        /// Vereinigt die externe mit der internen CommandBar
        /// </summary>
        /// <param name="bar">Die zu vereinigende CommandBar</param>
        /// <param name="commandBar">Fügt die Elemente der CommandBar am Ende an, sonst vorne</param>
        public void MergeCommandBar(CommandBar commandBar, bool tail = true)
        {
            foreach(var command in tail ? commandBar.PrimaryCommands : commandBar.PrimaryCommands.Reverse())
            {
                if (tail)
                {
                    CommandBar.PrimaryCommands.Add(command);
                }
                else
                {
                    CommandBar.PrimaryCommands.Insert(0, command);
                }
            }
        }

        /// <summary>
        /// Ermittelt die Koordinaten des Bereiches indem das Item gezeichnet werden soll
        /// </summary>
        /// <param name="infinity">Es gibt kienen Festen Bereich, indem das Item gezeichnet werden soll. 
        /// Stattdessen kann das Item den gesammten Zeichebreich nutzen</param>
        /// <returns>Das Rechteck, indem das Item gezeichnet werden soll</returns>
        protected Rect GetItemViewRect(out bool infinity)
        {
            var sz = new Size();
            var pt = new Point();

            var size = Item is ItemVisual ? (Item as ItemVisual).Size : new Size();

            if (size.IsEmpty || size.Equals(new Size()))  
            {
                // Keine Größe angegeben, Infinity-Modus wird aktiv
                sz = new Size(Content.ActualWidth, Content.ActualHeight);

                pt.X = (ScrollViewer.ScrollableWidth / 2) - (sz.Width / 2) - ScrollViewer.HorizontalOffset + (Content.ActualWidth / 2);
                pt.Y = (ScrollViewer.ScrollableHeight / 2) - (sz.Height / 2) - ScrollViewer.VerticalOffset + (Content.ActualHeight / 2);

                infinity = true;
            }
            else
            {
                sz = new Size(size.Width * Zoom / 100f, size.Height * Zoom / 100f);

                pt.X = (ScrollViewer.ScrollableWidth / 2) - (sz.Width / 2) - ScrollViewer.HorizontalOffset + (Content.ActualWidth / 2);
                pt.Y = (ScrollViewer.ScrollableHeight / 2) - (sz.Height / 2) - ScrollViewer.VerticalOffset + (Content.ActualHeight / 2);

                infinity = false;
            }

            return new Rect((int)pt.X, (int)pt.Y, (int)sz.Width, (int)sz.Height);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control seine Größe ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Content.Width = e.NewSize.Width;
            Content.Height = e.NewSize.Height;

            Invalidate();
        }

        /// <summary>
        /// Erstellt die Ressourcen
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnCreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            OnCreateResources(args);
        }

        /// <summary>
        /// Erstellt die Ressourcen
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected virtual void OnCreateResources(CanvasCreateResourcesEventArgs args)
        {
            Parallel.Invoke(() => { Item.CreateResources(Content); });
        }

        /// <summary>
        /// Zeichnet das Item
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDrawContent(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (Item == null) return;

            OnDrawContent(args);
        }

        /// <summary>
        /// Zeichnet das Item
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected virtual void OnDrawContent(CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinty);

            // Zeichne Hintergrund
            OnDrawBackground(args);

            Item.Update(new UpdateContext() { Designer = true });
            Item.Presentation(new PresentationContext(args.DrawingSession)
            {
                Designer = true,
                Matrix = Matrix3D.Identity * Matrix3D.Translation(viewRect.Left, viewRect.Top) * Matrix3D.Scaling(Zoom / 100f, Zoom / 100f)
            });

            args.DrawingSession.Flush();
        }

        /// <summary>
        /// Zeichnet den Hintergrund
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected virtual void OnDrawBackground(CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinty);
            var backround = Background is SolidColorBrush ? (Background as SolidColorBrush).Color : Color.FromArgb(255, 0, 0, 0);

            args.DrawingSession.FillRectangle(new Rect(0, 0, ActualWidth, ActualHeight), backround);

            if (GridVisibility == Visibility.Visible)
            {
                var lightGray = Color.FromArgb(80, 125, 125, 125);

                var alternated = ((int)(viewRect.Top / 10) + (int)(viewRect.Left / 10)) % 2 == 0;

                for (int y = (int)(viewRect.Top % 10) - 20; y < ActualHeight; y = y + 10)
                {
                    for (int x = (int)(viewRect.Left % 10) - (alternated ? 10 : 20); x < ActualWidth; x = x + 20)
                    {
                        args.DrawingSession.FillRectangle(new Rect(x, y, 10, 10), lightGray);
                    }

                    alternated = !alternated;
                }
            }
        }

        /// <summary>
        /// Zeichnet das vertikale Lineal
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDrawVerticalRuler(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinity);

            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = (Color)Application.Current.Resources["SystemChromeHighColor"];
            var black = Color.FromArgb(255, 0, 0, 0);

            args.DrawingSession.FillRectangle(new Rect(0, 0, VerticalRuler.ActualWidth, VerticalRuler.ActualHeight), lightGray);

            if (infinity)
            {
                args.DrawingSession.FillRectangle(new Rect(2, 0, VerticalRuler.ActualWidth - 4, VerticalRuler.ActualHeight), black);
            }
            else
            {
                args.DrawingSession.FillRectangle(new Rect(2, viewRect.Top, VerticalRuler.ActualWidth - 4, viewRect.Height), black);
            }

            int count = 0;

            for (int i = (int)viewRect.Top; i < VerticalRuler.ActualHeight; i += 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(3, i, (float)VerticalRuler.ActualWidth - 4, i, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(8, i, (float)VerticalRuler.ActualWidth - 8, i, white);
                }

                count++;
            }

            count = 1;

            for (int i = (int)viewRect.Top - 10; i > 0; i -= 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(3, i, (float)VerticalRuler.ActualWidth - 4, i, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(8, i, (float)VerticalRuler.ActualWidth - 8, i, white);
                }

                count++;
            }
        }

        /// <summary>
        /// Zeichnet das horizontale Lineal
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDrawHorizontalRuler(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinity);

            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = (Color)Application.Current.Resources["SystemChromeHighColor"];
            var black = Color.FromArgb(255, 0, 0, 0);

            args.DrawingSession.FillRectangle(new Rect(0, 0, HorizontalRuler.ActualWidth, HorizontalRuler.ActualHeight), lightGray);

            if (infinity)
            {
                args.DrawingSession.FillRectangle(new Rect(0, 2, HorizontalRuler.ActualWidth, HorizontalRuler.ActualHeight - 4), black);
            }
            else
            {
                args.DrawingSession.FillRectangle(new Rect(viewRect.Left, 2, viewRect.Width, HorizontalRuler.ActualHeight - 4), black);
            }

            int count = 0;

            for (int i = (int)viewRect.Left; i < HorizontalRuler.ActualWidth; i += 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(i, 3, i, (float)HorizontalRuler.ActualHeight - 4, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(i, 8, i, (float)HorizontalRuler.ActualHeight - 8, white);
                }

                count++;
            }

            count = 1;

            for (int i = (int)viewRect.Left - 10; i > 0; i -= 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(i, 3, i, (float)HorizontalRuler.ActualHeight - 4, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(i, 8, i, (float)HorizontalRuler.ActualHeight - 8, white);
                }

                count++;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Zoomfaktor geändert hat 
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnZoomValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs args)
        {
            Zoom = (int)args.NewValue;

            HorizontalRuler.Invalidate();
            VerticalRuler.Invalidate();
            Content.Invalidate();
        }

        /// <summary>
        /// Liefert oder setzt den Zoomwert
        /// </summary>
        public int Zoom
        {
            get { return (int)GetValue(ZoomProperty); }
            set
            {
                if (value < 10)
                {
                    SetValue(ZoomProperty, 10);
                }
                else if (value > 200)
                {
                    SetValue(ZoomProperty, 200);
                }
                else
                {
                    SetValue(ZoomProperty, value);
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            SizeChanged += OnSizeChanged;

            Content.Width = ActualWidth;
            Content.Height = ActualHeight;
        }

        // <summary>
        /// Wird aufgerufen, wenn das ScrollViewer geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnScrollViewerLoaded(object sender, RoutedEventArgs args)
        {
            ScrollViewer.ChangeView
            (
                ScrollViewer.ScrollableWidth / 2f,
                ScrollViewer.ScrollableHeight / 2f,
                1.0f,
                false
            );
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            UnregisterPropertyChangedCallback(GridVisibilityProperty, GridVisibilityPropertyToken);

            if (Item != null)
            {
                Item.PropertyChanged -= OnInvalidate;
            }

            if (ScrollViewer != null)
            {
                ScrollViewer.ViewChanged -= OnViewChanged;
                ScrollViewer.Loaded -= OnScrollViewerLoaded;
            }

            SizeChanged -= OnSizeChanged;
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Steuerelement neu gezeichnet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInvalidate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(int), typeof(EditorPanel), new PropertyMetadata(100));

        /// <summary>
        /// Liefert oder setzt den Scrollwert
        /// </summary>
        public int HorizontalScrollValue
        {
            get { return (int)GetValue(HorizontalScrollValueProperty); }
            set { SetValue(HorizontalScrollValueProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollValueProperty =
            DependencyProperty.Register("HorizontalScrollValue", typeof(int), typeof(EditorPanel), new PropertyMetadata(0));

        /// <summary>
        /// Liefert oder setzt den Scrollwert
        /// </summary>
        public int VerticalScrollValue
        {
            get { return (int)GetValue(VerticalScrollValueProperty); }
            set { SetValue(VerticalScrollValueProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty VerticalScrollValueProperty =
            DependencyProperty.Register("VerticalScrollValue", typeof(int), typeof(EditorPanel), new PropertyMetadata(0));

        /// <summary>
        /// Liefert oder setzt das Items
        /// </summary>
        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(Item), typeof(EditorPanel), new PropertyMetadata(null));

        /// <summary>
        /// Liefert oder setzt die Sichtbarbkeit des Rasters
        /// </summary>
        public Visibility GridVisibility
        {
            get { return (Visibility)GetValue(GridVisibilityProperty); }
            set
            {
                SetValue(GridVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty GridVisibilityProperty =
            DependencyProperty.Register("GridVisibility", typeof(Visibility), typeof(EditorPanel), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Liefert oder setzt die Sichtbarbkeit des Rasters
        /// </summary>
        public Visibility RulerVisibility
        {
            get { return (Visibility)GetValue(RulerVisibilityProperty); }
            set
            {
                SetValue(RulerVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty RulerVisibilityProperty =
            DependencyProperty.Register("RulerVisibility", typeof(Visibility), typeof(EditorPanel), new PropertyMetadata(Visibility.Visible));

    }
}
