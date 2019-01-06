using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Core.UIEditor
{
    public partial class BrushEditor : Form
    {
        private Brush m_brush;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public BrushEditor()
        {
            InitializeComponent();

            //Image = Icons.Properties.Resources.paintcan;
        }

        /// <summary>
        /// Wird beim erstmaligen Anzeigen der Form aufgerufen
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Auswahl ändert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckdChanged(object sender, EventArgs e)
        {
            if (sender == m_solidRadioButton)
            {
                Brush = new SolidBrush(Color.Yellow);
            }
            else if (sender == m_linearGradientRadioButton)
            {
                Brush = new LinearGradientBrush(new Point(), new Point(100, 100), Color.Blue, Color.Red);
            }
        }

        /// <summary>
        /// Wird zum Zeichnen der Vorschau aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewPaint(object sender, PaintEventArgs e)
        {
            if (Brush is Brush)
            {
                e.Graphics.FillRectangle(Brush, e.ClipRectangle);
            }
        }

        /// <summary>
        /// Liefert oder setzt den Brush
        /// </summary>
        public Brush Brush 
        { 
            get
            {
                return m_brush;
            } 

            set
            {
                m_brush = value;

                m_propertyGrid.SelectedObject = m_brush;

                m_previewPanel.Refresh();
            }
        }

        private void OnPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            m_previewPanel.Refresh();
        }

        


                
    }
}
