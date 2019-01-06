
using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Adventure.Items
{
    [XmlType("image")]
    public class ItemImageUfo : ItemVisualImage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemImageUfo()
            : base(new ItemVisualImageContext())
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "UFO";
            Image = global::GameExpress.Adventure.Properties.Resources.ufo;
        }

    }
}
