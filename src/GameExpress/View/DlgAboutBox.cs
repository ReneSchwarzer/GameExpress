using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameExpress.Controller;

namespace GameExpress.View
{
    public partial class DlgAboutBox : Form, IView<IControllerAbout>
    {
        /// <summary>
        /// Liefert oder setzt den Controller
        /// </summary>
        private IControllerAbout Controller { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DlgAboutBox()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Verknüfpft den Controller mit der View
        /// </summary>
        /// <param name="controller">Der zugehörige Controller</param>
        public void SetController(IControllerAbout controller)
        {
            Controller = controller;
        }

        /// <summary>
        /// Tritt ein, wenn die Form geladen wird 
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Text = String.Format("Info über {0}", Controller.AssemblyTitle);
            this.labelProductName.Text = Controller.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", Controller.AssemblyVersion);
            this.labelCopyright.Text = Controller.AssemblyCopyright;
            this.labelCompanyName.Text = Controller.AssemblyCompany;
            this.textBoxDescription.Text = Controller.AssemblyDescription;
        }
    }
}
