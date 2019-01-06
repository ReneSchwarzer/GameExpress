using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExpress.Core
{
    /// <summary>
    /// Schnittstelle für ein Projekt
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        void Init();

        /// <summary>
        /// Liefert das Wurzelelement
        /// </summary>
        ItemRoot RootItem { get; set; }
    }
}
