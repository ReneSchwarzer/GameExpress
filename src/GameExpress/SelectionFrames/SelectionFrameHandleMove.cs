using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Vector = GameExpress.Model.Structs.Vector;

namespace GameExpress.SelectionFrames
{
    public class SelectionFrameHandleMove : SelectionFrameHandle
    {
        /// <summary>
        /// Liefert oder setzt die Verschiebung der x-Achse entlang
        /// </summary>
        private short OriginalTranslationX { get; set; }

        /// <summary>
        /// Liefert oder setzt die Verschiebung der y-Achse entlang
        /// </summary>
        private short OriginalTranslationY { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Anker</param>
        public SelectionFrameHandleMove(ISelectionFrame frame, ISelectionFrameAnchor anchor)
            : base(frame, anchor, Orbit.Low)
        {
            Width = Height *= 2;
        }

        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawOverlay(PresentationContext pc)
        {
            var lightGray = (Color)Application.Current.Resources["SystemChromeHighColor"];

            var p = CurrentPosition;

            var width = (Width / 13f);
            var l4 = (float)p.X - width * 5f;
            var l3 = (float)p.X - width * 3f;
            var l2 = (float)p.X - width * 2f;
            var l1 = (float)p.X - width;
            var h = (float)p.X;
            var r1 = (float)p.X + width;
            var r2 = (float)p.X + width * 2f;
            var r3 = (float)p.X + width * 3f;
            var r4 = (float)p.X + width * 5f;

            var height = (Height / 13f);
            var t4 = (float)p.Y - height * 5f;
            var t3 = (float)p.Y - height * 3f;
            var t2 = (float)p.Y - height * 2f;
            var t1 = (float)p.Y - height;
            var v = (float)p.Y;
            var b1 = (float)p.Y + height;
            var b2 = (float)p.Y + height * 2f;
            var b3 = (float)p.Y + height * 3f;
            var b4 = (float)p.Y + height * 5f;

            using (var geometry = CanvasGeometry.CreatePolygon(pc.Graphics, new Vector2[]
            {
                new Vector2(h, t4),
                new Vector2(r2, t3),
                new Vector2(r1, t3),
                new Vector2(r1, t1),
                new Vector2(r3, t1),
                new Vector2(r3, t2),
                new Vector2(r4, v),
                new Vector2(r3, b2),
                new Vector2(r3, b1),
                new Vector2(r1, b1),
                new Vector2(r1, b3),
                new Vector2(r2, b3),
                new Vector2(h, b4),
                new Vector2(l2, b3),
                new Vector2(l1, b3),
                new Vector2(l1, b1),
                new Vector2(l3, b1),
                new Vector2(l3, b2),
                new Vector2(l4, v),
                new Vector2(l3, t2),
                new Vector2(l3, t1),
                new Vector2(l1, t1),
                new Vector2(l1, t3),
                new Vector2(l2, t3),
                new Vector2(h, t4)
            }))
            {
                pc.Graphics.FillGeometry(geometry, lightGray);
            }
        }

        /// <summary>
        /// Zeichnet die Dekoration
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawDecoration(PresentationContext pc)
        {
            var p1 = CurrentPosition;

            var p2 = Anchor.GetHandle(Orbit.None).CurrentPosition;
            var foreground = new UISettings().GetColorValue(UIColorType.Foreground);

            pc.Graphics.DrawLine((float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y, foreground, 1);
        }

        /// <summary>
        /// Beginnt mit dem Einfangen des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        public override void CaptureBegin(Vector capturePoint)
        {
            base.CaptureBegin(capturePoint);

            if (Item is IItemTranslation item)
            {
                OriginalTranslationX = item.TranslationX;
                OriginalTranslationY = item.TranslationY;
            }
        }

        /// <summary>
        /// Verschieben des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param>
        /// <param name="matrix">Die Matrix mit den Transformationseigenschaften</param>
        public override void CaptureDrag(Vector capturePoint, Matrix3D matrix)
        {
            base.CaptureDrag(capturePoint, matrix);

            var m = matrix;

            if (Item is IItemVisual graphics)
            {
                m *= graphics.GetMatrix();
            }

            m = m.Invert;

            // Verschiebung anwenden
            if (Item is IItemTranslation item)
            {
                var dir = m.Transform(capturePoint) - m.Transform(CapturePoint);

                item.TranslationX = (short)(OriginalTranslationX + dir.X);
                item.TranslationY = (short)(OriginalTranslationY + dir.Y);
            }
        }
    }
}
