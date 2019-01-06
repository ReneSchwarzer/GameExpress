using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using GameExpress.Core.Structs;

namespace GameExpress.Core.Items
{
    public class ItemVisualKeyFrameTweening : ItemVisualKeyFrame
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualKeyFrameTweening()
            : base(null)
        {
            Name = "TweeningFrame_" + GUID;
            Matrix = Matrix3D.Identity;
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            base.Presentation(pc);
        }
    }
}
