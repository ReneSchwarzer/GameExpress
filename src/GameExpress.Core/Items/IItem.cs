using GameExpress.Core.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace GameExpress.Core.Items
{
    public interface IItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich Namenseigenschaften geändert haben
        /// </summary>
        event EventHandler<ItemEventArgs> NameChanged;

        /// <summary>
        /// Liefert das Kontextobjekt
        /// </summary>
        ItemContext Context { get; }

        /// <summary>
        /// Liefert oder setzt den Name des Items
        /// </summary>
	    string Name { get; set; }

	    /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
	    string Note { get; set; }

        /// <summary>
        /// Liefert Informationen zu dem Item
        /// </summary>
	    string Info { get; }

	    /// <summary>
        /// Liefert die eindeutige ID
        /// </summary>
	    ulong GUID { get; set; }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        void Update(UpdateContext uc);

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        void Presentation(PresentationContext pc);

        /// <summary>
        /// Sucht ein Item anahnd des Namens
        /// </summary>
        /// <param name="name">Name des gesuchten Items</param>
        /// <param name="type">Der Typ des gesuchten Items oder null für alle Typen</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        IItem FindItem(string name, bool oneLevel = false);

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        IItem Copy();
    }
}
