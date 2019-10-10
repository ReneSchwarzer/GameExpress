using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.SelectionFrames
{
    public interface ISelectionFrameHandle
    {
        /// <summary>
        /// Liefert oder setzt den zugehörigen Verweis auf das Item 
        /// </summary>
        Item Item { get; set; }

        /// <summary>
        /// Liefert oder setzt den zugehörigen Anker
        /// </summary>
        ISelectionFrameAnchor Anchor { get; }

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Liefert die Höhe
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Liefert den Orbit
        /// </summary>
        Orbit Orbit { get; }

        /// <summary>
        /// Liefert oder setzt ob das Handle auf einen höheren Orbit befördert werden soll
        /// </summary>
        bool HightOrbit { get; set; }

        /// <summary>
        /// Liefert oder setzt die Koordinaten, die beim letzten Zeichnen vorlagen
        /// </summary>
        Vector CurrentPosition { get; }

        /// <summary>
        /// Handle aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        void Update(UpdateContext uc);

        /// <summary>
        /// Zeichnet den Handle
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        void Presentation(PresentationContext pc);

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        ISelectionFrameHandle HitTest(HitTestContext hc, Vector point);

        /// <summary>
        /// Wird beim Anklicken des Handles aufgerufen
        /// </summary>
        /// <param name="point">Die Koordinaten des Pointers</param> 
        void OnPointerPressed(Vector point);

        /// <summary>
        /// Wird aufgerufen, wenn innerhalb des Steuerelements die Position des Zeigegerätes sich ändert
        /// </summary>
        /// <param name="point">Die Koordinaten des Pointers</param> 
        /// <param name="matrix">Die Matrix mit den Transformationseigenschaften</param>
        void OnPointerMoved(Vector point, Matrix3D matrix);

        /// <summary>
        /// Wird aufgerufen, wenn das Zeigegerät nicht mehr gedrückt wird
        /// </summary>
        void OnPointerReleased();

        /// <summary>
        /// Beginnt mit dem Einfangen des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        void CaptureBegin(Vector capturePoint);

        /// <summary>
        /// Verschieben des Handles
        /// </summary>
        /// <param name="capturePoint">Die Koordinaten des Pointers</param> 
        /// <param name="matrix">Die Matrix mit den Transformationseigenschaften</param>
        void CaptureDrag(Vector capturePoint, Matrix3D matrix);

        /// <summary>
        /// Beendet das Einfangen des Handles. Beim Abbruch sind die ursprünglichen Werte wiederherzustellen.
        /// </summary>
        /// <param name="cancel">Der Vorgang wurde abgebrochen.</param> 
        void CaptureEnd(bool cancel = false);
    }
}
