
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
    public class ItemObjectState1 : ItemVisualAnimatedObjectState
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemObjectState1()
            : base()
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Name = "Objektzustand1";

            EndTime = 160;
            Loop = Loop.Default;

            var itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 1",
                Item = "Sputnik",
                Enable = true,
                Lock = true
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 20, Matrix = Matrix3D.Translation(120, 0) * Matrix3D.Scaling(0.1f, 0.1f), Tweening = true });
            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Zweites Schlüsselbild", From = 110, Duration = 50, Matrix = Matrix3D.Translation(-10, 0) * Matrix3D.Scaling(0.1f, 0.1f) });

            InstanceItems.Add(itemInstance);

            itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 2",
                Item = "UFO",
                Enable = true,
                Lock = false
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 10, Matrix = Matrix3D.Translation(10, 10) * Matrix3D.Scaling(0.2f, 0.2f), Tweening = true });
            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Zweites Schlüsselbild", From = 150, Duration = 10, Matrix = Matrix3D.Translation(150, 150) * Matrix3D.Scaling(0.1f, 0.1f) });

            InstanceItems.Add(itemInstance);

            itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 3",
                Item = "Omicron",
                Enable = true,
                Lock = false
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 160, Matrix = Matrix3D.Translation(50, 50) * Matrix3D.Scaling(0.2f, 0.2f), Tweening = true });
            InstanceItems.Add(itemInstance);

            itemInstance = new ItemVisualInstance()
            {
                Name = "Ebene 4",
                Item = "Objektzustand2",
                Enable = true,
                Lock = false
            };

            itemInstance.KeyFrames.Add(new ItemVisualKeyFrame() { Name = "Erstes Schlüsselbild", From = 0, Duration = 160, Matrix = Matrix3D.Translation(0, 130) * Matrix3D.Scaling(0.9f, 0.9f), Tweening = true });
            InstanceItems.Add(itemInstance);
        }

    }
}
