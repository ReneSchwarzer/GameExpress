using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace GameExpress.SelectionFrames
{
    public class SelectionFrameHandleHotSpot : SelectionFrameHandle
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Anker</param>
        public SelectionFrameHandleHotSpot(ISelectionFrame frame, ISelectionFrameAnchor anchor)
            : base(frame, anchor)
        {
            Width = Height = 20;
        }

        /// <summary>
        /// Zeichnet den Hintergrund
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawBackground(PresentationContext pc)
        {
            var p = pc.Transform(new Point(0, 0));
            var accent1 = new UISettings().GetColorValue(UIColorType.AccentLight1);
            accent1.A = 125;

            pc.Graphics.FillEllipse((float)p.X, (float)p.Y, Width / 2, Height / 2, accent1);
        }

        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawOverlay(PresentationContext pc)
        {
            // Hotspot zeichnen
            var p = pc.Transform(new Point(0, 0));

            var accent = new UISettings().GetColorValue(UIColorType.Accent);
            var foreground = new UISettings().GetColorValue(UIColorType.Foreground);

            if (pc.Level == 1)
            {
                pc.Graphics.DrawLine((float)p.X - 5, (float)p.Y, (float)p.X + 5, (float)p.Y, accent, 3);
                pc.Graphics.DrawLine((float)p.X, (float)p.Y - 5, (float)p.X, (float)p.Y + 5, accent, 3);
                pc.Graphics.DrawLine((float)p.X - 5, (float)p.Y, (float)p.X + 5, (float)p.Y, foreground, 1);
                pc.Graphics.DrawLine((float)p.X, (float)p.Y - 5, (float)p.X, (float)p.Y + 5, foreground, 1);
            }

        }
    }
}
