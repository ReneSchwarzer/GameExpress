using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Drawing;

namespace GameExpress.Core.UIEditor
{
    public class TransparencyUITypeEditor : UITypeEditor
    {
        /**
         * Gibt an, ob der angegebene Kontext das Zeichnen einer Objektwertdarstellung innerhalb des angegebenen Kontexts unterstützt.
         * 
         * @param Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können. 
         * @return true, wenn PaintValue implementiert ist, andernfalls false.
         */
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /**
         * Zeichnet die Darstellung eines Objektwerts mit dem angegebenen PaintValueEventArgs.
         * 
         * @param e Eine PaintValueEventArgs-Klasse, die die zu zeichnenden Werte und den Zeichenbereich angibt. 
         */
        public override void PaintValue(PaintValueEventArgs e)
        {
            int normalX = (e.Bounds.Width / 2);
            int normalY = (e.Bounds.Height / 2);

            if (e.Value.GetType() != typeof(Structs.Transparency))
            {
                return;
            }
            Structs.Transparency t = (Structs.Transparency)e.Value;
            
            if (t.Enable)
            {
                // Hintergrund füllen
                e.Graphics.FillRectangle(new SolidBrush(t.Color), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            }
            else
            {
                // Hintergrund füllen
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                Region r = e.Graphics.Clip;
                System.Drawing.Drawing2D.CompositingQuality q = e.Graphics.CompositingQuality;
                
                e.Graphics.Clip = new Region(e.Bounds);
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                
                // Rechteck durchstreichen
                Pen p = new Pen(new SolidBrush(Color.Red), 2);
                e.Graphics.DrawLine(p, 0, 0, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawLine(p, 0, e.Bounds.Height, e.Bounds.Width, 0);
                
                p.Dispose();
                
                e.Graphics.Clip = r;
                e.Graphics.CompositingQuality = q;
            }
        }

        /**
         * Ruft den Editor-Stil ab, der von der EditValue-Methode verwendet wird.
         * 
         * @param context Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.
         * @return Ein UITypeEditorEditStyle-Wert, der den von der EditValue-Methode verwendeten Editor-Stil angibt. Wenn UITypeEditor diese Methode nicht unterstützt, gibt GetEditStyle den Wert None zurück.
         */
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.None;
        }
    }
}
