using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("directory")]
    public class ItemDirectory : Item
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemDirectory()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemDirectory)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">das zugehörige Kontextobjekt</param>
        public ItemDirectory(ItemContext context)
            :base(context, true)
        {
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            return base.Copy();
        }
    }
}
