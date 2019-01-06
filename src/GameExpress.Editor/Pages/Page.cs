using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace GameExpress.Editor.Pages
{
    /// <summary>
    /// Klasse repräsentiert ein leeres Control, welches weitere Steuerelemente aufnehmen kann
    /// </summary>
    public partial class Page : UserControl
    {
        /// <summary>
        /// Zeigt an, dass sich das Bild geändert hat
        /// </summary>
        [Category("Layout"), Browsable(true)]
        public event EventHandler ImageChanged;

        /// <summary>
        /// Zeigt an, dass sich der Titel geändert hat
        /// </summary>
        [Category("Layout"), Browsable(true)]
        public event EventHandler TitleChanged;

        /// <summary>
        /// Zeigt an, dass sich der Titel geändert hat
        /// </summary>
        [Category("Layout"), Browsable(true)]
        public event EventHandler<StatusChangeEventArgs> StatusChange;

        /// <summary>
        /// Bild
        /// </summary>
        private Image m_image;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Page()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        /// <summary>
        /// Wird aufgerufen, wenn gedruckt werden soll
        /// </summary>
        /// <returns>Das Druckdokument</returns>
        public virtual PrintDocument Print()
        {
            return null;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Daten exportiert werden sollen
        /// </summary>
        /// <returns>Die zum Exportieren aufbereiteten Daten</returns>
        public virtual List<List<string>> ExportData()
        {
            return null;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Status ändert
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStatusChanged(StatusChangeEventArgs e)
        {
            if (StatusChange != null)
            {
                StatusChange(this, e);
            }
        }

        /// <summary>
        /// Threadsicherer Zugriff
        /// </summary>
        /// <param name="a"></param>
        public void ExecuteSecure(Action a)
        {
            if (InvokeRequired)
            {
                Invoke(a);
            }
            else
            {
                a();
            }
        }

        /// <summary>
        /// Liefert oder setzt das mit dem Control verbundene Bild
        /// </summary>
        [Category("Appearance")]
        public virtual Image Image
        {
            get
            {
                return this.m_image;
            }
            set
            {
                if (this.m_image != value)
                {
                    this.m_image = value;
                    if (this.ImageChanged != null)
                    {
                        this.ImageChanged(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Text 
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// Liefert oder setzt den Titel 
        /// </summary>
        [Category("Appearance")]
        public virtual string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                if (this.Text != value)
                {
                    this.Text = value;
                    if (this.TitleChanged != null)
                    {
                        this.TitleChanged(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// Liefert ob die Page Drucken untertützt
        /// </summary>
        public bool IsPrintable { get; protected set; }

        /// <summary>
        /// Liefert ob die Page Exportieren untertützt
        /// </summary>
        public bool IsExportable { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die mit der Seite verknüften Daten
        /// </summary>
        public object Data { get; set; }
    }
}
