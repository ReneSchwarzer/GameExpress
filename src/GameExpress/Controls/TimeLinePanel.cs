using GameExpress.Model.Item;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace GameExpress.Controls
{
    public sealed class TimeLinePanel : Control
    {
        /// <summary>
        /// Liefert oder setzt das Lineal
        /// </summary>
        private CanvasControl Ruler { get; set; }

        /// <summary>
        /// Liefert oder setzt die Tabelle
        /// </summary>
        private Grid Table { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeitmarkierung
        /// </summary>
        private TimePosition TimePosition { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die derigistrierung benötigt wird
        /// </summary>
        private long TimePropertyToken { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TimeLinePanel()
        {
            this.DefaultStyleKey = typeof(TimeLinePanel);
            Time = 0;
        }


        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Ruler = GetTemplateChild("Ruler") as CanvasControl;

            if (Ruler != null)
            {
                Ruler.Draw += OnDrawRuler;
                Ruler.PointerPressed += OnRulerPointerPressed;
                Ruler.PointerMoved += OnPointerMoved;
                Ruler.PointerReleased += OnPointerReleased;
                //Window.Current.CoreWindow.PointerMoved += OnRulerPointerMoved;
            }

            Table = GetTemplateChild("Table") as Grid;

            if (Table != null)
            {
                Table.DataContext = this;
                //Table.ItemsSource = Instances;
            }

            TimePosition = GetTemplateChild("TimePosition") as TimePosition;

            if (TimePosition != null)
            {
                TimePosition.DataContext = this;
            }

            // Eigenschaft TimeProperty hat sich geändert
            TimePropertyToken = RegisterPropertyChangedCallback(TimeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Ruler?.Invalidate();
                TimePosition.Time = Time;
            }));
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            UnregisterPropertyChangedCallback(TimeProperty, TimePropertyToken);

            Unloaded -= OnUnloaded;
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Zeigegerät, was vorher gedrückt wurde, losgelassen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Zeigegerät bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnRulerPointerMoved(CoreWindow sender, PointerEventArgs args)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät über das Lineal gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRulerPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Ruler);
            if (pointer.IsInContact)
            {
                Time = (ulong)pointer.Position.X;
            }

            e.Handled = true;

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät über das Lineal bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Ruler);
            if (pointer.IsInContact)
            {
                Time = (ulong)pointer.Position.X;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Zeichnet das Lineal
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDrawRuler(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = Color.FromArgb(255, 125, 125, 125);
            var black = Color.FromArgb(255, 0, 0, 0);
            var accent = new UISettings().GetColorValue(UIColorType.AccentDark3);

            args.DrawingSession.FillRectangle(0, 0, (float)Ruler.ActualWidth, (float)Ruler.ActualHeight, lightGray);

            int count = 0;

            for (int i = 0; i < (int)Ruler.ActualWidth; i += 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(i, 3, i, (float)Ruler.ActualHeight - 4, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(i, 8, i, (float)Ruler.ActualHeight - 8, white);
                }

                count++;
            }
        }

        /// <summary>
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Time.
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(new ulong()));

        /// <summary>
        /// Liefert oder setzt des Items
        /// </summary>
        public ObservableCollection<ItemInstance> Instances
        {
            get { return (ObservableCollection<ItemInstance>)GetValue(InstancesProperty); }
            set { SetValue(InstancesProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Instances.
        /// </summary>
        public static readonly DependencyProperty InstancesProperty =
            DependencyProperty.Register("Instances", typeof(ObservableCollection<ItemInstance>), typeof(TimeLinePanel), new PropertyMetadata(null));

    }
}
