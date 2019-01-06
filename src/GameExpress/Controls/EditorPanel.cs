using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

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
        /// Konstruktor
        /// </summary>
        public EditorPanel()
        {
            DefaultStyleKey = typeof(EditorPanel);
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

            // Eigenschaft GridVisibilityProperty hat sich geändert
            RegisterPropertyChangedCallback(GridVisibilityProperty, new DependencyPropertyChangedCallback((s, e) => 
            {
                // Neuzeichnen erforderlich
                Content?.Invalidate();
            }));

            // Eigenschaften des Items haben sich geändert
            if (Item != null)
            {
                Item.PropertyChanged += (s, e) =>
                {
                    // Neuzeichnen erforderlich
                    Content?.Invalidate();
                };
            }
            
        }

        /// <summary>
        /// Zwingt das Control zum neuzeichnen
        /// </summary>
        public virtual void Invalidate()
        {
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

            if (size.IsEmpty)
            {
                // Keine Größe angegeben, Infinity-Modus wird aktiv
                sz.Width = 1000;
                sz.Height = 1000;

                pt.X = 0; // - HorizontalScrollBar.Value;
                pt.Y = 0;// - VerticalScrollBar.Value;

                infinity = true;
            }
            else
            {
                sz = new Size(size.Width * Zoom / 100f, size.Height * Zoom / 100f);

                pt.X = (Content.ActualWidth / 2) - (sz.Width / 2); // - HorizontalScrollBar.Value;
                pt.Y = (Content.ActualHeight / 2) - (sz.Height / 2); // - VerticalScrollBar.Value;

                infinity = false;
            }

            return new Rect((int)pt.X, (int)pt.Y, (int)sz.Width, (int)sz.Height);
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
        }

        /// <summary>
        /// Zeichnet den Hintergrund
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected virtual void OnDrawBackground(CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out bool infinty);

            if (GridVisibility == Visibility.Visible)
            {
                var lightGray = Color.FromArgb(80, 125, 125, 125);

                var alternated = false;
                for (int y = (int)(viewRect.Top % 10) - 20; y < Content.ActualHeight; y = y + 10)
                {
                    for (int x = (int)(viewRect.Left % 10) - (alternated ? 10 : 20); x < Content.ActualWidth; x = x + 20)
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
            bool infinity;
            var viewRect = GetItemViewRect(out infinity);

            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = Color.FromArgb(255, 125, 125, 125);
            var black = Color.FromArgb(255, 0, 0, 0);

            if (infinity)
            {
                args.DrawingSession.FillRectangle(new Rect(0, 0, VerticalRuler.ActualWidth, VerticalRuler.ActualHeight), black);
            }
            else
            {
                args.DrawingSession.FillRectangle(new Rect(0, 0, VerticalRuler.ActualWidth, VerticalRuler.ActualHeight), lightGray);
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
            bool infinity;
            var viewRect = GetItemViewRect(out infinity);

            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = Color.FromArgb(255, 125, 125, 125);
            var black = Color.FromArgb(255, 0, 0, 0);

            if (infinity)
            {
                args.DrawingSession.FillRectangle(new Rect(0, 0, HorizontalRuler.ActualWidth, HorizontalRuler.ActualHeight), black);
            }
            else
            {
                args.DrawingSession.FillRectangle(new Rect(0, 0, HorizontalRuler.ActualWidth, HorizontalRuler.ActualHeight), lightGray);
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
        /// Liefert oder setzt des Items
        /// </summary>
        public Item Item
        {
            get { return (Item)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
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
