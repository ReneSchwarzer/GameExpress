using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GameExpress.Controls
{
    /// <summary>
    /// Hilfsklasse zum Drag & Drop und Größenänderungen innerhalb eines Fensters
    /// </summary>
    public class SelectionHelper<T> where T : Item
    {
        /// <summary>
        /// Die verschiedenen Änderungsarten
        /// </summary>
        public enum SelectionEditMode { Move, From, Duration }

        /// <summary>
        /// Liefert oder setzt das ausgewählte Item
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Liefert oder setzt die orignalen Koordinaten, die bei der Auswahl vorlagen
        /// </summary>
        public Point OriginalPosition { get; set; }

        /// <summary>
        /// Liefert oder setzt die orignalen Koordinaten des Items, die bei der Auswahl vorlagen
        /// </summary>
        public Point OriginalItemPosition { get; set; }

        /// <summary>
        /// Liefert oder setzt den Änderungsmodus
        /// </summary>
        public SelectionEditMode EditMode { get; set; }

        /// <summary>
        /// Ist true, wenn sich das Zeigegerät außerhalb des aktuellen Steuerelementes befindet
        /// </summary>
        public bool Outside { get; set; }
    }
}
