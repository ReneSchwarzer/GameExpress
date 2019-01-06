using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExpress.Adventure.Items
{
    public class ItemRoot : GameExpress.Core.Items.ItemRoot
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemRoot(ItemContext context)
            :base(context)
        {

        }
    }
}
