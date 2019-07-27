using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.SelectionFrames
{
    public interface ISelectionFrame
    {
        /// <summary>
        /// Liefert oder setzt den zugehörigen Verweis auf das Item 
        /// </summary>
        Item Item { get; }

        /// <summary>
        /// Auswahlrahmen aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        void Update(UpdateContext uc);

        /// <summary>
        /// Zeichnet den Auswahlrahmen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        void Presentation(PresentationContext pc);

        /// <summary>
        /// Wechselt das Toolstet
        /// </summary>
        void SwitchToolset();

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        ISelectionFrameHandle HitTest(HitTestContext hc, Vector point);

        /// <summary>
        /// Liefert einen Anker
        /// </summary>
        /// <param name="location">Die Position des Ankers</param>
        /// <returns>Der Anker oder null</returns>
        ISelectionFrameAnchor GetAnchor(Location location);
    }
}
