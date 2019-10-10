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
using Vector = GameExpress.Model.Structs.Vector;

namespace GameExpress.SelectionFrames
{
    public class SelectionFrameHandleSizeMove : SelectionFrameHandle
    {
        /// <summary>
        /// Liefert oder setzt die Ausgangsposition
        /// </summary>
        private Vector OriginalPosition { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Anker</param>
        public SelectionFrameHandleSizeMove(ISelectionFrame frame, ISelectionFrameAnchor anchor)
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

            if (Item is ItemMapVertext vertext)
            {
                OriginalPosition = vertext.Vector;
            }
        }

        /// <summary>
        /// Verschieben des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param>
        /// <param name="matrix">Die Matrix mit den Transformationseigenschaften</param>
        public override void CaptureDrag(Model.Structs.Vector capturePoint, Matrix3D matrix)
        {
            base.CaptureDrag(capturePoint, matrix);

            var m = matrix.Invert;

            // Verschiebung anwenden
            if (Item is ItemMapVertext vertext)
            {
                var dir = m.Transform(capturePoint) - m.Transform(CapturePoint);
                vertext.Vector = OriginalPosition + dir;
            }
        }
    }
}
