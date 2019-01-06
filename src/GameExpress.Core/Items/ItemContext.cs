using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public abstract class ItemContext : IItemContext
    {
        static private ulong m_count = 0;        //< Nächste ID

        /// <summary>
        /// Item-Erzeugung
        /// </summary>
        /// <returns>Das neue Item</returns>
        public abstract Item ItemFactory();

        /// <summary>
        /// Stellt fest, ob der Typ ein Unterobjekt des aktuellen Items sein kann
        /// </summary>
        /// <param name="type">Der zu überprüfende Typ</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public virtual bool Accept(Type type)
        {
            return true;
        }

        /// <summary>
        /// Liefert die nächste ID
        /// </summary>
        /// <returns>Die neue ID</returns>
        static public ulong NextGUID()
        {
            return ++m_count;
        }

        /// <summary>
        /// Liefert den allgemeinen Namen
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Liefert das allgemeine Symbol
        /// </summary>
        public abstract Image Image { get; }

        /// <summary>
        /// Gibt an, ob die Items im Baum angezeigt werden
        /// </summary>
        public abstract bool Hidden { get; }
    }
}
