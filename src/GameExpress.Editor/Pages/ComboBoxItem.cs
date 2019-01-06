using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    public class ComboBoxItem<T>
    {
        /// <summary>
        /// Liefert oder setzt den (Anzeige)Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Wandelt das Objekt in einen String um
        /// </summary>
        /// <returns>Die Stringräpresentation des Objektes</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
