using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Model
{
    public interface IModelAbout : IModel
    {
        /// <summary>
        /// Liefert den Namen des Assemblys
        /// </summary>
        string AssemblyTitle { get; }

        /// <summary>
        /// Liefert die Version des Assemblys
        /// </summary>
        string AssemblyVersion { get; }

        /// <summary>
        /// Liefert die Beschreibung des Assemblys
        /// </summary>
        string AssemblyDescription { get; }

        /// <summary>
        /// Liefert die Produktbeschreibung des Assemblys
        /// </summary>
        string AssemblyProduct { get; }

        /// <summary>
        /// Liefert Copyrightinformationen des Assemblys
        /// </summary>
        string AssemblyCopyright { get; }

        /// <summary>
        /// Liefert die Organisation des Assemblys
        /// </summary>
        string AssemblyCompany { get; }
    }
}
