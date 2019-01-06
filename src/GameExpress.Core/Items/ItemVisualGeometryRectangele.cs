using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("rectangele")]
    public class ItemVisualGeometryRectangele : ItemVisualGeometry
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualGeometryRectangele()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualGeometryRectangele)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualGeometryRectangele(ItemContext context)
            :base(context)
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void DrawGeometry(Structs.PresentationContext pc)
        {
            // Nichts zum zeichnen vorhanden
	        if (Size.IsEmpty) return;

            ImageAttributes imageAtt = new ImageAttributes();	
	        Point[] destPoints = { new Point(0, 0), new Point(Size.Width, 0), new Point(0, Size.Height) };

	        // Punkte transformieren
	        pc.Transform(destPoints);
	    
            // Bildattribute bestimmen
	        pc.SetImageArrtibut(imageAtt);
	
	        // Rechteck zeichnen
            using (Pen pen = new Pen(FrontColor, StrokeWidth))
            {
                Rectangle rect = new Rectangle(destPoints[0], new Size(destPoints[1].X - destPoints[0].X, destPoints[2].Y - destPoints[0].Y));

                if (Transparency.Enable)
                {
                    pc.Graphics.DrawRectangle(pen, rect);
                }
                else
                {
                    using (Brush brush = new SolidBrush(BackColor))
                    {
                        pc.Graphics.FillRectangle(brush, rect);
                        pc.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }
        }
    }
}
