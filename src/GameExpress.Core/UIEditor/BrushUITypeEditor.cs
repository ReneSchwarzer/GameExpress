using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Core.UIEditor
{
    public class BrushUITypeEditor : UITypeEditor
    {
        /// <summary>
        /// Ruft den Editor-Stil ab, der von der EditValue-Methode verwendet wird.
        /// </summary>
        /// <param name="context">Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>Ein UITypeEditorEditStyle-Wert, der den von der EditValue-Methode verwendeten Editor-Stil angibt. Wenn UITypeEditor diese Methode nicht unterstützt, gibt GetEditStyle den Wert None zurück.</returns>
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Bearbeitet den Wert des angegebenen Objekts, wobei der von der GetEditStyle-Methode angegebene Editor-Stil verwendet wird.
        /// </summary>
        /// <param name="context">Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <param name="provider">Ein IServiceProvider, über den dieser Editor Dienste anfordern kann.</param>
        /// <param name="value">Das zu bearbeitende Objekt.</param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using(var dlg = new BrushEditor())
            {
                dlg.Brush = value as Brush;

                if (dlg.ShowDialog() == DialogResult.OK)
                {

                }
            }

            return base.EditValue(context, provider, value);
        }

        /// <summary>
        /// Gibt an, ob der angegebene Kontext das Zeichnen einer Objektwertdarstellung innerhalb des angegebenen Kontexts unterstützt.
        /// </summary>
        /// <param name="context">Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>true, wenn PaintValue implementiert ist, andernfalls false.</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Zeichnet die Darstellung eines Objektwerts mit dem angegebenen PaintValueEventArgs.
        /// </summary>
        /// <param name="e"> Eine PaintValueEventArgs-Klasse, die die zu zeichnenden Werte und den Zeichenbereich angibt. </param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value is Brush)
            {
                e.Graphics.FillRectangle(e.Value as Brush, e.Bounds);
            }
        }
    }
}
