using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Core.Items
{
    public class ItemVisualSceneContext : ItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <param name="item">Das mit der Ansicht verbundene Item</param>
        /// <returns>Ein neues Item</returns>
        public override Item ItemFactory()
        {
            return new ItemVisualObject(this);
        }

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">Der zu überprüfende Type</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public override bool Accept(Type type)
        {
            return (type.Equals(typeof(ItemRoot))) ? false : true; ;
        }

        /// <summary>
        /// Name des Items
        /// </summary>
        public override string Name
        {
            get { return "Szene"; }
        }

        /// <summary>
        /// Liefert das Symbol
        /// </summary>
        public override Image Image
        {
            get { return Properties.Resources.item_scene; }
        }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public override bool Hidden { get { return false; } }
    }
}
