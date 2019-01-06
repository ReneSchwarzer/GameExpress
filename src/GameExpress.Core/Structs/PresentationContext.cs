using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace GameExpress.Core.Structs
{
    
    public class PresentationContext
    {
	    /// <summary>
	    /// Konstruktor
	    /// </summary>
	    public PresentationContext()
        {
            Time = new Structs.Time();
            Matrix = Matrix3D.Identity;
            Level = 1;
        }

        /// <summary>
        /// Kopier - Konstruktor
        /// </summary>
        /// <param name="g">Zeichenkontext</param>
        public PresentationContext(Graphics g)
            :this()
        {
            Graphics = g;
        }
	    
        /// <summary>
        /// Kopier - Konstruktor
        /// </summary>
        /// <param name="pc">Der Presentation Kontext</param>
	    public PresentationContext(PresentationContext pc)
            :this()
        {
            Designer = pc.Designer;
            Transparency = pc.Transparency;
            Hue = pc.Hue;
            Alpha = pc.Alpha;
            Matrix = pc.Matrix; 
            Level = pc.Level+1;
            Time = pc.Time;

            Graphics = pc.Graphics;
        }

        /// <summary>
        /// Alphawert hinzufügen
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        public void AddAlpha(Alpha alpha)
        {
            Alpha.Add(alpha);
        }

        /// <summary>
        /// Gammawert hinzufügen
        /// </summary>
        /// <param name="gamma">Der Gammawert</param>
        public void AddGamma(Gamma gamma) 
        {
            //Gamma << gamma;
            //m_gamma = gamma->m_gamma;
        }
        
        /// <summary>
        /// Fabtonwert hinzufügen
        /// </summary>
        /// <param name="hue">Der Farbtonwert</param>
        public void AddHue(Hue hue) 
        {
           // m_hue << &hue;
            //m_enable = (ft->m_enable) ? true : m_enable;
            //if (ft->m_enable)
            //{
            //    BYTE red = GetRValue(m_color) + (BYTE)((255.0f - GetRValue(m_color)) * ((double)GetRValue(ft->m_color) / 255.0f));
            //    BYTE green = GetGValue(m_color) + (BYTE)((255.0f - GetGValue(m_color)) * ((double)GetGValue(ft->m_color) / 255.0f));
            //    BYTE blue = GetBValue(m_color) + (BYTE)((255.0f - GetBValue(m_color)) * ((double)GetBValue(ft->m_color) / 255.0f));
            //    m_color = RGB(red, green, blue);
            //    m_alpha += (BYTE)((255.0f - (double)m_alpha) * ((double)ft->m_alpha / 255.0f));
            //}
        }
    	
        /// <summary>
        /// Transparenz hinzufügen
        /// </summary>
        /// <param name="transparency">Der Transparenzwert</param>
        public void AddTransparency(Transparency transparency) 
        {
            Transparency = transparency;
            //Transparency.Enable = (transparency.Enable) ? true : Transparency.Enable;
            //Transparency.Color = transparency.Color;
        }

        /// <summary>
        /// Transformiert Punkte
        /// </summary>
        /// <param name="points">Array von Points</param>
        public void Transform(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Matrix.Transform(points[i]);
            }
        }

        /// <summary>
        /// Transformiert ein Punkt
        /// </summary>
        /// <param name="point">Der zu transformierende Punkt</param>
        public Point Transform(Point point)
        {
            return Matrix.Transform(point);
        }

        /// <summary>
        /// Image Attribute festlegen
        /// </summary>
        /// <param name="imageAtt">Ein- und Ausgangsparameter mit den Bildattributen</param>
        public void SetImageArrtibut(ImageAttributes imageAtt) 
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
                                          { 
                                            new float[]{1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, 
                                            new float[]{0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                            new float[]{0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                            new float[]{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                                            new float[]{0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                          });
            if (Hue.Enable) 
            {
                float ft = Hue.Alpha / 255.0f;
    		
                colorMatrix[0, 0] = 1.0f - ft;
                colorMatrix[1, 1] = 1.0f - ft;
                colorMatrix[2, 2] = 1.0f - ft;
                colorMatrix[3, 3] = (1.0f - (Alpha / 255.0f));
                colorMatrix[4, 0] = (Hue.Color.R / 255.0f * ft);
                colorMatrix[4, 1] = (Hue.Color.G / 255.0f * ft);
                colorMatrix[4, 2] = (Hue.Color.B / 255.0f * ft);

                imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            } 
            else 
            {
                colorMatrix[3, 3] = (1.0f - (Alpha / 255.0f));
                imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }
    	
            // Transparenz 
            if (Transparency.Enable) 
            {
                imageAtt.SetColorKey(Transparency.Color, Transparency.Color, ColorAdjustType.Bitmap);
            }

            // Gamma
            //imageAtt.SetGamma(Gamma, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// Der Präsentationskontext wird im Designer ausgeführt
        /// </summary>
        public bool Designer { get; set; }

        /// <summary>
        /// Transparenzobjekt
        /// </summary>
        private Transparency Transparency { get; set; }	

        /// <summary>
        /// Farbtonobjekt
        /// </summary>
        private Hue Hue { get; set; }

        /// <summary>
        /// Alpha
        /// </summary>
        private Alpha Alpha { get; set; }

        /// <summary>
        /// Gamma
        /// </summary>
        private Gamma Gamma { get; set; }	

        /// <summary>
        /// eine 3x3 Matrix
        /// </summary>
        public Matrix3D Matrix { get; set; }

        /// <summary>
        /// Tiefe
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        public Time Time { get; set; }

        /// <summary>
        /// Zeichenkontext
        /// </summary>
        public Graphics Graphics { get; protected set; }
    }
}
