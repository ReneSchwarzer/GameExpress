using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GameExpress.Core.Items;

namespace GameExpress.Core
{
    public class ItemContextList
    {
        /// <summary>
        /// Die Kontextliste
        /// </summary>
        private List<Tuple<Type, ItemContext>> m_contextList = new List<Tuple<Type, ItemContext>>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemContextList()
        {
        }

        /// <summary>
        /// Registriert ein Kontextobjekt
        /// </summary>
        /// <param name="type">Das zu registrierende Objekt</param>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public void RegisterItemContext(Type type, ItemContext context)
        {
            if (type != null)
            {
                Tuple<Type, ItemContext> del = null;

                // Bestehenden Eintrag suchen
                foreach (var item in m_contextList)
                {
                    if (item.Item1.Equals(type))
                    {
                        del = item;
                        break;
                    }
                }

                if (del != null)
                {
                    m_contextList.Remove(del);
                }

                // neuer Eintrag
                var i = new Tuple<Type, ItemContext>(type, context);
                m_contextList.Add(i);
            }
        }

        /// <summary>
        /// Liefert ein Kontext
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <returns>Der Kontext</returns>
        public ItemContext GetItemContext(Type type)
        {
            foreach (var i in m_contextList)
            {
                if (i.Item1.Equals(type) || type.IsSubclassOf(i.Item1) || i.Item1.IsSubclassOf(type))
                {
                    return i.Item2;
                }
            }

            return null;
        }

        /// <summary>
        /// Gibt ein Enumerator zurück, mit den die Auflistung durchlaufen werden kann
        /// </summary>
        /// <returns>Enumerator der Liste</returns>
        public List<Tuple<Type, ItemContext>>.Enumerator GetEnumerator()
        {
            return m_contextList.GetEnumerator();
        }
    }
}
