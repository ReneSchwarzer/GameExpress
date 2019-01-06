using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GameExpress.Editor.Pages
{
    public partial class Panel : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Panel()
        {
            InitializeComponent();
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ContainerControl | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            UpdateStyles();
        }
    }
}
