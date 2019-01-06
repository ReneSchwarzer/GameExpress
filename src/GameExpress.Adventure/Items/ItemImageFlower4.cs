
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
    public class ItemImageFlower4 : ItemVisualImage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemImageFlower4()
            : base(new ItemVisualImageContext())
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Blume4";
            Image = global::GameExpress.Adventure.Properties.Resources.blume4;
            Transparency = new GameExpress.Core.Structs.Transparency(Color.Magenta, true);
        }

    }
}
