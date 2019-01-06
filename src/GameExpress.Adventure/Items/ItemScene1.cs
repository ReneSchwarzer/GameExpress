
using GameExpress.Core.Items;
using GameExpress.Core.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Adventure.Items
{
    [XmlType("scene")]
    public class ItemScene1 : ItemVisualScene
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemScene1()
            : base()
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Szene1";

            var itemInstance = new ItemVisualInstance()
            {
                Name = "Hintergrund",
                Item = "Hintergrund_Szene1",
                Enable = true,
                Lock = true
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame()
            {
                Name = "Erstes Schlüsselbild",
                From = 0,
                Duration = ulong.MaxValue,
                Matrix = Matrix3D.Translation(0, 0) * Matrix3D.Scaling(0.336f, 0.336f)
            });
            
            InstanceItems.Add(itemInstance);
        }

    }
}
