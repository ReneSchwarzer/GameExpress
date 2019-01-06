﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public class ItemVisualImageContext : ItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>Das erstelle Item</returns>
        public override Item ItemFactory()
        {
            return new ItemVisualImage(this);
        }

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">Der zu überprüfende Typ</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public override bool Accept(Type type)
        {
            return false;
        }

        /// <summary>
        /// Liefert den allgemeinen Namen
        /// </summary>
        public override string Name 
        {
            get { return "Bild"; } 
        }

        /// <summary>
        /// Liefert das allgemeine Symbol
        /// </summary>
        public override Image Image
        {
            get { return Properties.Resources.item_image; }
        }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public override bool Hidden { get { return false; } }
    }
}
