using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GameExpress.Adventure.Items;

namespace GameExpress.Adventure.Items
{
    public class ItemRootContext : GameExpress.Core.Items.ItemRootContext
    {
        /**
         * Item-Erzeugung
         * 
         * @return ein neues Item 
         */
        public override GameExpress.Core.Items.Item ItemFactory()
        {
            return new ItemRoot(this);
        }

        /**
         * Name des Items
         */
        public override string Name 
        {
            get { return "Adventure"; } 
        }

        ///**
        // * Liefert das Symbol
        // */
        //public override Image Image
        //{
        //    get { return null; }
        //}
    }
}
