using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    //[XmlType("project")]
    public class ItemRoot : Item
    {
        /// <summary>
        /// Die Weite des Spielbereiches
        /// </summary>
        private int m_width;

        /// <summary>
        /// Die Höhe des Spielbereiches
        /// </summary>
        private int m_height;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemRoot()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemRoot)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemRoot(ItemContext context)
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

        /// <summary>
        /// Liefert oser setzt die Weite des Spielbereiches
        /// </summary>
        [XmlAttribute("width")]
        public int Width
        {
            get { return m_width; }
            set
            {
                if (!m_width.Equals(value))
                {
                    m_width = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oser setzt die Höhe des Spielbereiches
        /// </summary>
        [XmlAttribute("height")]
        public int Height
        {
            get { return m_height; }
            set
            {
                if (!m_height.Equals(value))
                {
                    m_height = value;

                    NotifyPropertyChanged();
                }
            }
        }
    }
}
