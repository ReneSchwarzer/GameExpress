using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace GameExpress.SelectionFrames
{
    public class SelectionFrameHandleSizeE : SelectionFrameHandle
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Anker</param>
        public SelectionFrameHandleSizeE(ISelectionFrame frame, ISelectionFrameAnchor anchor)
            : base(frame, anchor)
        {
        }

        /// <summary>
        /// Zeichnet den Hintergrund
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawBackground(PresentationContext pc)
        {
            base.DrawBackground(pc);

            //var accent = new UISettings().GetColorValue(UIColorType.Accent);
            //var foreground = new UISettings().GetColorValue(UIColorType.Foreground);

            //var p0 = pc.Transform(new Point(Width, Height / 2f));
            //var p1 = pc.Transform(new Point(0, 0));
            //var p2 = pc.Transform(new Point(0, Height));

            //using (var geometry = CanvasGeometry.CreatePolygon(pc.Graphics, new Vector2[]
            //{
            //    new Vector2((float)p0.X, (float)p0.Y),
            //    new Vector2((float)p1.X, (float)p1.Y),
            //    new Vector2((float)p2.X, (float)p2.Y)
            //}))
            //{
            //    pc.Graphics.FillGeometry(geometry, accent);
            //    pc.Graphics.DrawGeometry(geometry, foreground, 1);
            //}
        }

        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawOverlay(PresentationContext pc)
        {

        }
    }
}
