using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.SelectionFrames
{
    /// <summary>
    /// Ein Auswahlrahmen
    /// </summary>
    public class SelectionFrameImage : SelectionFrame<ItemImage>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit dem Auswahlrahmen verbundene Item</param>
        public SelectionFrameImage(ItemImage item)
            : base(item)
        {
            //Anchors.Add(new SelectionFrameHandleAnchorHotSpot(this));
        }

        /// <summary>
        /// Zeichnet den Auswahlrahmen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            var m = pc.Matrix;

            base.Presentation(pc);

            pc.Matrix = m;
        }
    }
}
