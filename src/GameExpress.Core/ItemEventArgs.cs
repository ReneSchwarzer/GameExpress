using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExpress.Core
{
    public class ItemEventArgs : System.EventArgs
    {
        #region Members

        /** Variablen */
        IItem m_item;

        #endregion

        #region Methoden

        /**
         * Konstruktor
         *
         * @param item Item
         */
        public ItemEventArgs(IItem item)
        {
            m_item = item;
        }

        #endregion

        #region Eigenschaften

        /**
         * Liefert das Item
         */
        public IItem Item
        {
            get { return m_item; }
        }

        #endregion
    }
}
