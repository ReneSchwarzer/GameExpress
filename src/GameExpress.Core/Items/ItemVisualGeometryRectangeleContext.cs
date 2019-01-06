using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public class ItemVisualGeometryRectangeleContext : ItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>ein neues Item</returns>
        public override Item ItemFactory()
        {
            return new ItemVisualGeometryRectangele(this);
        }

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">der zu überprüfende Type</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public override bool Accept(Type type)
        {
            return type.IsSubclassOf(typeof(ItemVisualGeometry));
        }

        /// <summary>
        /// Name des Items
        /// </summary>
        public override string Name 
        {
            get { return "Rechteck"; } 
        }

        /// <summary>
        /// Liefert das Symbol
        /// </summary>
        public override Image Image
        {
            get { return Properties.Resources.item_image; }
        }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public override bool Hidden { get { return false; } }
    }
}
