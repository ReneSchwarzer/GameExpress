using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public interface IItemContext
    {
        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>Das neue Item</returns>
        Item ItemFactory();

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">Der zu überprüfende Typ</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        bool Accept(Type type);

        /// <summary>
        /// Liefert den allgemeinen Namen
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Liefert das allgemeine Symbol
        /// </summary>
        Image Image { get; }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        bool Hidden { get; }
    }
}
