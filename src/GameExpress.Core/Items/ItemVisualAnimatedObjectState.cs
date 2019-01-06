using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using System.Linq;
using GameExpress.Core.Structs;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    /// <summary>
    /// Objektzustand
    /// </summary>
    [XmlType("state")]
    public class ItemVisualAnimatedObjectState : ItemVisualAnimated
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualAnimatedObjectState()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualAnimatedObjectState)))
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualAnimatedObjectState(ItemContext context)
            :base(context, true)
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(pc);

            DrawHotspot(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualAnimatedObjectState;

            return copy;
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public override Size Size
        {
            get
            {
                return new Size();
            }
        }
    }
}
