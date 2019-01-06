using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameExpress.Core
{
    /**
     * Schnittstelle für die Erzeugung eines Projektes
     */
    public interface IProjectContext
    {
        /**
         * Prüft die Kompatibilität der verschiedenen Versionen
         * 
         * @raram version
         * @return true wenn Kompatibel, sonst false
         */
        bool CheckVersion(ushort version);

        /**
         * erstellt ein neues Projekt
         * 
         * @return das neue Projekt
         */
        IProject CreateProject();

        /**
         * Liefert die Version des Projektes
         */
        ushort Version { get; }

        /**
         * Liefert den Namen des Projektes
         */
        string Name { get; }

        /**
         * Liefert weitere Informationen zu dem Projekt
         */
        string Info { get; }

        /**
         * Liefert das Symbol des Projektes
         */
        Image Image { get; }
    }
}
