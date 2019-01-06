
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
    [XmlType("map")]
    public class ItemMap1 : ItemMap
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMap1()
            : base()
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Pfad";
            AddChild(new ItemMapVertext() { Point = new Point(310, 310), Name = "V1" });
            AddChild(new ItemMapVertext() { Point = new Point(340, 390), Name = "V2" });
            AddChild(new ItemMapVertext() { Point = new Point(420, 330), Name = "V3" });
            AddChild(new ItemMapVertext() { Point = new Point(520, 350), Name = "V4" });

            AddChild(new ItemMapMesh() { Vertext1 = "V1", Vertext2 = "V2", Vertext3 = "V3" });
            AddChild(new ItemMapMesh() { Vertext1 = "V2", Vertext2 = "V3", Vertext3 = "V4" });
        }

    }
}
