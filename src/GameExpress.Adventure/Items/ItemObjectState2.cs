
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
    [XmlType("state")]
    public class ItemObjectState2 : ItemVisualAnimatedObjectState
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemObjectState2()
            : base()
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Objektzustand2";

            EndTime = 110;
            Loop = Loop.Oscillate;

            var itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 1",
                Item = "Blume1",
                Enable = true,
                Lock = true
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 10, Matrix = Matrix3D.Translation(0, 0) * Matrix3D.Scaling(0.4f, 0.4f), Tweening = true });
            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Zweites Schlüsselbild", From = 100, Duration = 10, Matrix = Matrix3D.Translation(100, 0) * Matrix3D.Scaling(0.4f, 0.4f) });

            InstanceItems.Add(itemInstance);

            itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 2",
                Item = "Blume2",
                Enable = true,
                Lock = false
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 10, Matrix = Matrix3D.Translation(40, 0) * Matrix3D.Scaling(0.4f, 0.4f), Tweening = true });
            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Zweites Schlüsselbild", From = 100, Duration = 10, Matrix = Matrix3D.Translation(140, 0) * Matrix3D.Scaling(0.5f, 0.5f) });

            InstanceItems.Add(itemInstance);
        }

    }
}
