using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Imaging;

namespace GameExpress.Core.UIEditor
{
    public class HueUITypeEditor : UITypeEditor
    {
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
            int normalX = (e.Bounds.Width / 2);
            int normalY = (e.Bounds.Height / 2);

            if (e.Value.GetType() != typeof(Structs.Hue))
            {
                return;
            }
            Structs.Hue h = (Structs.Hue)e.Value;

            if (h.Enable)
            {
                Bitmap image = new Bitmap(e.Bounds.Width, e.Bounds.Height);
                Graphics g = Graphics.FromImage(image);
                ColorMatrix colorMatrix = new ColorMatrix(); // 5x5 Einheitsmatrix
                ImageAttributes imageAtt = new ImageAttributes();

                float ft = h.Alpha / 255.0f;

                colorMatrix.Matrix00 = 1.0f - ft;
                colorMatrix.Matrix11 = 1.0f - ft;
                colorMatrix.Matrix22 = 1.0f - ft;
                colorMatrix.Matrix33 = 1.0f - ft;
                colorMatrix.Matrix40 = ((h.Color.R) / 255.0f * ft);
                colorMatrix.Matrix41 = ((h.Color.G) / 255.0f * ft);
                colorMatrix.Matrix42 = ((h.Color.B) / 255.0f * ft);

                imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                Color c = Color.FromArgb((int)((h.Color.R) / 255.0f * ft), (int)((h.Color.G) / 255.0f * ft), (int)((h.Color.B) / 255.0f * ft));

                // Bildhintergrund füllen
                g.FillRectangle(new SolidBrush(h.Color), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                //e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                // Hintergrund füllen
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawImage(image, e.Bounds, 0, 0, e.Bounds.Width, e.Bounds.Height, GraphicsUnit.Pixel, imageAtt);

                g.Dispose();
                image.Dispose();
            }
            else
            {
                Region r = e.Graphics.Clip;
                System.Drawing.Drawing2D.CompositingQuality q = e.Graphics.CompositingQuality;

                e.Graphics.Clip = new Region(e.Bounds);
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Hintergrund füllen
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                // Rechteck durchstreichen
                Pen p = new Pen(new SolidBrush(Color.Red), 2);
                e.Graphics.DrawLine(p, 0, 0, e.Bounds.Width, e.Bounds.Height);
                e.Graphics.DrawLine(p, 0, e.Bounds.Height, e.Bounds.Width, 0);

                p.Dispose();

                e.Graphics.Clip = r;
                e.Graphics.CompositingQuality = q;
            }
        }

        /// <summary>
        /// Ruft den Editor-Stil ab, der von der EditValue-Methode verwendet wird.
        /// </summary>
        /// <param name="context">Eine ITypeDescriptorContext-Schnittstelle, über die zusätzliche Kontextinformationen abgerufen werden können.</param>
        /// <returns>Ein UITypeEditorEditStyle-Wert, der den von der EditValue-Methode verwendeten Editor-Stil angibt. Wenn UITypeEditor diese Methode nicht unterstützt, gibt GetEditStyle den Wert None zurück.</returns>
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.None;
        }
    }
}
