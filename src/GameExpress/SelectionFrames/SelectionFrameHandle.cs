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
    public class SelectionFrameHandle : ISelectionFrameHandle
    {
        /// <summary>
        /// Liefert oder setzt den zugehörigen Verweis auf das Item 
        /// </summary>
        public Item Item { get; set; }

        // <summary>
        /// Liefert oder setzt den Verweis auf den zurgehörigen Frame
        /// </summary>
        protected ISelectionFrame Frame { get; private set; }

        /// <summary>
        /// Der Orbit
        /// </summary>
        private Orbit _orbit { get; set; } = Orbit.None;

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Width { get; protected set; } = 14;

        /// <summary>
        /// Liefert oder setzt die Höhe
        /// </summary>
        public int Height { get; protected set; } = 14;

        /// <summary>
        /// Liefert oder setzt die Koordinaten, die beim letzten Zeichnen vorlagen
        /// </summary>
        public Vector CurrentPosition { get; private set; } = Vector.Invalid;

        /// <summary>
        /// Die Koordinaten der Zeigegerätes
        /// </summary>
        protected Vector CapturePoint { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Orbit
        /// </summary>
        public Orbit Orbit
        {
            get
            {
                if (!HightOrbit)
                {
                    return _orbit;
                }

                switch (_orbit)
                {
                    case Orbit.None:
                        return Orbit.Low;
                    case Orbit.Low:
                        return Orbit.Medium;
                    case Orbit.Medium:
                        return Orbit.Height;
                    default:
                        return _orbit;
                }
            }
            set => _orbit = value;
        }

        /// <summary>
        /// Liefert oder setzt ob das Handle auf einen höheren Orbit befördert werden soll
        /// </summary>
        public bool HightOrbit { get; set; } = false;

        /// <summary>
        /// Liefert oder setzt den zugehörigen Anker
        /// </summary>
        protected ISelectionFrameAnchor Anchor { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Frame</param>
        /// <param name="orbit">Der Orbit</param>
        public SelectionFrameHandle(ISelectionFrame frame, ISelectionFrameAnchor anchor, Orbit orbit)
        {
            Frame = frame;
            Anchor = anchor;
            Orbit = orbit;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Verweis auf den zugehörigen Frame</param>
        /// <param name="anchor">Der zugehörige Frame</param>
        public SelectionFrameHandle(ISelectionFrame frame, ISelectionFrameAnchor anchor)
            : this(frame, anchor, Orbit.None)
        {
        }

        /// <summary>
        /// Handle aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public virtual void Update(UpdateContext uc)
        {
            CurrentPosition = uc.Transform(new Vector(0f));


        }

        /// <summary>
        /// Zeichnet den Handle
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void Presentation(PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            DrawDecoration(pc);
            DrawBackground(pc);
            DrawOverlay(pc);
        }

        /// <summary>
        /// Zeichnet den Hintergrund
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawBackground(PresentationContext pc)
        {
            var accent = new UISettings().GetColorValue(UIColorType.Accent);
            var foreground = new UISettings().GetColorValue(UIColorType.Foreground);

            var p = CurrentPosition;

            pc.Graphics.FillCircle(new Vector2((float)p.X, (float)p.Y), Height / 2f, foreground);
            pc.Graphics.DrawCircle(new Vector2((float)p.X, (float)p.Y), Height / 2f, accent, 1);
        }

        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawOverlay(PresentationContext pc)
        {

        }

        /// <summary>
        /// Zeichnet die Dekoration
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawDecoration(PresentationContext pc)
        {
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        public virtual ISelectionFrameHandle HitTest(HitTestContext hc, Vector point)
        {
            var p = CurrentPosition;
            var rect = new Rect(new Point(p.X - Width / 2f, p.Y - Height / 2f), new Size(Width, Height));

            return rect.Contains(point) ? this : null;
        }

        /// <summary>
        /// Beginnt mit dem Einfangen des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        public virtual void CaptureBegin(Vector capturePoint)
        {
            CapturePoint = capturePoint;
        }

        /// <summary>
        /// Verschieben des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        /// <param name="matrix">Die Matrix mit den Transformationseigenschaften</param>
        public virtual void CaptureDrag(Vector capturePoint, Matrix3D matrix)
        {
        }

        /// <summary>
        /// Beendet das Einfangen des Handles. Beim Abbruch sind die ursprünglichen Werte wiederherzustellen.
        /// </summary>
        /// <param name="cancel">Der Vorgang wurde abgebrochen.</param> 
        public virtual void CaptureEnd(bool cancel = false)
        {
            CapturePoint = Vector.Invalid;
        }
    }
}
