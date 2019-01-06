using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameExpress.Model;
using GameExpress.View;

namespace GameExpress.Controller
{
    /// <summary>
    /// Controller
    /// </summary>
    public class ControllerAbout : IControllerAbout
    {
        /// <summary>
        /// Die zugehörige View
        /// </summary>
        private IView<IControllerAbout> View { get; set; }

        /// <summary>
        /// Das zugehörige Model
        /// </summary>
        private IModelAbout Model { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">Die View</param>
        /// <param name="model">Das Modell</param>
        public ControllerAbout(IView<IControllerAbout> view, IModelAbout model)
        {
            View = view;
            Model = model;

            View.SetController(this);
            //Model.attach((IModelObserver)view);
            //View.changed += new ViewHandler<IView>(this.view_changed);
        }

        /// <summary>
        /// Liefert den Namen des Assemblys
        /// </summary>
        public string AssemblyTitle { get { return Model.AssemblyTitle; } }

        /// <summary>
        /// Liefert die Version des Assemblys
        /// </summary>
        public string AssemblyVersion { get { return Model.AssemblyVersion; } }

        /// <summary>
        /// Liefert die Beschreibung des Assemblys
        /// </summary>
        public string AssemblyDescription { get { return Model.AssemblyDescription; } }

        /// <summary>
        /// Liefert die Produktbeschreibung des Assemblys
        /// </summary>
        public string AssemblyProduct { get { return Model.AssemblyProduct; } }

        /// <summary>
        /// Liefert Copyrightinformationen des Assemblys
        /// </summary>
        public string AssemblyCopyright { get { return Model.AssemblyCopyright; } }

        /// <summary>
        /// Liefert die Organisation des Assemblys
        /// </summary>
        public string AssemblyCompany { get { return Model.AssemblyCompany; } }
    }
}
