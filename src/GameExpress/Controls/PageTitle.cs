using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameExpress.Editor.Pages;

namespace GameExpress.Controls
{
    public partial class PageTitle : Page
    {
        /// <summary>
        /// Liefert oder setzt das mit dem Control verbundene Bild
        /// </summary>
        [Category("Appearance")]
        public override Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
                m_inageLabel.Image = Image;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Titel 
        /// </summary>
        [Category("Appearance")]
        public override string Title
        {
            get
            {
                return base.Title;
            }
            set
            {
                base.Title = value;
                m_titleLabel.Text = Title;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTitle()
        {
            InitializeComponent();
        }
    }
}
