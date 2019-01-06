using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public class ItemVisualKeyFrameContext : ItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>Ein neues Item</returns>
        public override Item ItemFactory()
        {
            return new ItemVisualKeyFrame(this);
        }

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">Der zu überprüfende Type</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public override bool Accept(Type type)
        {
            return false;
        }

        /// <summary>
        /// Name des Items
        /// </summary>
        public override string Name 
        {
            get { return "Schlüsselbild"; } 
        }

        /// <summary>
        /// Liefert das Symbol
        /// </summary>
        public override Image Image
        {
            get { return Properties.Resources.item_object; }
        }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public override bool Hidden { get { return true; } }
    }
}
