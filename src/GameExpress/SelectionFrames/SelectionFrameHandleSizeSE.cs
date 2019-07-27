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
    public class SelectionFrameHandleSizeSE : SelectionFrameHandle
    {
        /// <summary>
        /// Die Skalierung der x-Achse
        /// </summary>
        private double OriginalScaleX { get; set; }

        /// <summary>
        /// Die Skalierung der y-Achse
        /// </summary>
        private double OriginalScaleY { get; set; }

        private Vector OriginalSize { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Anker</param>
        public SelectionFrameHandleSizeSE(ISelectionFrame frame, ISelectionFrameAnchor anchor)
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
        }

        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void DrawOverlay(PresentationContext pc)
        {

        }

        /// <summary>
        /// Beginnt mit dem Einfangen des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        public override void CaptureBegin(Vector capturePoint)
        {
            base.CaptureBegin(capturePoint);

            if (Item is IItemScale item && Item is IItemVisual visual)
            {
                OriginalScaleX = item.ScaleX;
                OriginalScaleY = item.ScaleY;
                OriginalSize = new Vector(visual.Size.Width * item.ScaleX / 100f, visual.Size.Height * item.ScaleY / 100f);
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

            var dir = m.Transform(capturePoint) - m.Transform(CapturePoint);

            var x = dir.X * 100f / OriginalSize.X; 

            //var xy = capturePoint - Frame.GetAnchor(Location.NorthWest).CurrentPosition;

            // Skalierung anwenden
            if (Item is IItemScale item)
            {
                // Skalierungsfaktor in %
                //item.ScaleX = xy.X * OriginalScaleX / OrginalSize.X;
                //item.ScaleY = xy.Y * OriginalScaleY / OrginalSize.Y;

                item.ScaleX = OriginalScaleX + x * 1.9f;
            }
        }
    }
}
