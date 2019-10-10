using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.SelectionFrames
{
    public interface ISelectionFrameAnchor
    {
        /// <summary>
        /// Event zum Abfragen der aktuellen Position
        /// </summary>
        event EventHandler<EventArgsRetrievePosition> RetrievePosition;

        /// <summary>
        /// Liefert die Position
        /// </summary>
        Location Location { get; }

        /// <summary>
        /// Liefert die Werkzeuge
        /// </summary>
        ObservableCollection<ISelectionFrameHandle> Handles { get; }

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
        /// Liefert ein Handle, weches sich auf dem angegeben Orbit befindet
        /// </summary>
        /// <param name="orbit">Der Orbit</param>
        /// <returns>Der Handle oder null</returns>
        ISelectionFrameHandle GetHandle(Orbit orbit);
    }
}
