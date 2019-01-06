using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("Scene")]
    public class ItemVisualScene : ItemVisualAnimatedObjectState
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualScene()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualScene)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualScene(ItemContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            base.Presentation(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualScene;
            
            return copy;
        }
    }
}
