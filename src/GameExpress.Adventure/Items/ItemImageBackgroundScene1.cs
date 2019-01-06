
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
    public class ItemImageBackgroundScene1 : ItemVisualImage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemImageBackgroundScene1()
            : base()
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Hintergrund_Szene1";
            Image = global::GameExpress.Adventure.Properties.Resources.scene1;
        }

    }
}
