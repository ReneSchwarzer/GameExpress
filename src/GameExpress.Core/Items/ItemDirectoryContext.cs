using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public class ItemDirectoryContext : ItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>ein neues Item </returns>
        public override Item ItemFactory()
        {
            return new ItemDirectory(this);
        }

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">der zu überprüfende Typ</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public override bool Accept(Type type)
        {
            return (type.Equals(typeof(ItemRoot))) ? false : true;
        }

        /// <summary>
        /// Name des Items
        /// </summary>
        public override string Name 
        {
            get { return "Verzeichnis"; } 
        }
        
        /// <summary>
        /// Liefert das Symbol
        /// </summary>
        public override Image Image
        {
            get { return Properties.Resources.item_directory; }
        }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public override bool Hidden { get { return false; } }
    }
}
