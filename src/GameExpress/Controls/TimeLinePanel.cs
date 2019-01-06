using GameExpress.Model.Item;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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


            // Eigenschaft TimeProperty hat sich geändert
            RegisterPropertyChangedCallback(TimeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Ruler?.Invalidate();
            }));

            // Eigenschaft InstancesProperty hat sich geändert
            RegisterPropertyChangedCallback(InstancesProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                FillInstanceLsit();
            }));

            FillInstanceLsit();
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
            var darkRed = Color.FromArgb(255, 80, 0, 0);

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

            // Zeitpositionsmarke anzeigen
            var x = Time;

            if (x <= Ruler.ActualWidth)
            {
                var p = new Vector2[4];
                p[0] = new Vector2((int)x - 5, 2);
                p[1] = new Vector2(p[0].X + 10, 2);
                p[2] = new Vector2(p[0].X + (p[1].X - p[0].X) / 2, 16);
                p[3] = new Vector2(p[0].X, p[0].Y);

                var geometry = CanvasGeometry.CreatePolygon(args.DrawingSession, p);
                args.DrawingSession.FillGeometry(geometry, darkRed);
            }

        }

        /// <summary>
        /// Füllt die Liste der Instanzen
        /// </summary>
        private void FillInstanceLsit()
        {
            var head = Table.RowDefinitions.FirstOrDefault();
            Table.RowDefinitions.Clear();

            Table.RowDefinitions.Add(head);
            int i = 1;

            foreach(var r in Instances)
            {
                Table.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25, GridUnitType.Pixel) });

                var label = new TextBlock() { Text = r.Name };
                Table.Children.Add(label);
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, i);

                i++;
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
            DependencyProperty.Register("TimeProperty", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(new ulong()));

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
