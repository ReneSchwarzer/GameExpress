using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameExpress.Controller;
using GameExpress.Model;
using GameExpress.View;

namespace GameExpress
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            
            var view = new FormMain();
            var controlller = new ControllerMain(view, new ModelMain());

            Application.Run(view);
        }
    }
}
