using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Controls
{
    public partial class PropertyGrid : System.Windows.Forms.PropertyGrid
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PropertyGrid()
        {
            InitializeComponent();

            DoubleBuffered = true;

            //SetStyle(style | ControlStyles.OptimizedDoubleBuffer, true);
            //UpdateStyles();
        }
    }
}
