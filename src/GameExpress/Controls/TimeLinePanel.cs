using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Numerics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExpress.Controls
{
    public sealed class TimeLinePanel : Control
    {
        /// <summary>
        /// Liefert oder setzt das Lineal
        /// </summary>
        private CanvasControl Ruler { get; set; }

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
            }
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
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("TimeProperty", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(0));

    }
}
