using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace GameExpress.Core.Structs
{
    /// <summary>
    /// Matrix für 2-Dimensionale Verformungsberechnungen
    /// </summary>
    [XmlType("matrix")]
    public struct Matrix3D
    {
        /// <summary>
        /// Elemente der Matrix
        /// </summary>
        private float m_11, m_12, m_13;
        private float m_21, m_22, m_23;
        private float m_31, m_32, m_33;	
	
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="m11"></param>
        /// <param name="m12"></param>
        /// <param name="m13"></param>
        /// <param name="m21"></param>
        /// <param name="m22"></param>
        /// <param name="m23"></param>
        /// <param name="m31"></param>
        /// <param name="m32"></param>
        /// <param name="m33"></param>
        public Matrix3D(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
        {
            m_11 = m11;
            m_12 = m12;
            m_13 = m13;
            m_21 = m21;
            m_22 = m22;
            m_23 = m23;
            m_31 = m31;
            m_32 = m32;
            m_33 = m33; 
        }

        /// <summary>
        /// Kopierkonstruktor
        /// </summary>
        /// <param name="matrix">Die zu kopierende Matrix</param>
        public Matrix3D(Matrix3D matrix)
           : this(matrix.M11, matrix.M12, matrix.M13, matrix.M21, matrix.M22, matrix.M23, matrix.M31, matrix.M32, matrix.M33)
        {

        }

	    // Arithmetische Operatoren
	    public static Matrix3D operator + (Matrix3D m1, Matrix3D m2) 
        { 
            return new Matrix3D(m1.m_11 + m2.m_11, m1.m_12 + m2.m_12, m1.m_13 + m2.m_13, m1.m_21 + m2.m_21, m1.m_22 + m2.m_22, m1.m_23 + m2.m_23, m1.m_31 + m2.m_31, m1.m_32 + m2.m_32, m1.m_33 + m2.m_33); 
        }

        /// <summary>
        /// Arithmetische Operaton - Subtraktion 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix3D operator - (Matrix3D m1, Matrix3D m2)
        {
            return new Matrix3D(m1.m_11 - m2.m_11, m1.m_12 - m2.m_12, m1.m_13 - m2.m_13, m1.m_21 - m2.m_21, m1.m_22 - m2.m_22, m1.m_23 - m2.m_23, m1.m_31 - m2.m_31, m1.m_32 - m2.m_32, m1.m_33 - m2.m_33);
        }

        /// <summary>
        /// Multiplikation zweier Matritzen
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix3D operator *(Matrix3D m1, Matrix3D m2)
        {
            return new Matrix3D(m1.m_11 * m2.m_11 + m1.m_21 * m2.m_12 + m1.m_31 * m2.m_13,
                                m1.m_12 * m2.m_11 + m1.m_22 * m2.m_12 + m1.m_32 * m2.m_13,
                                m1.m_13 * m2.m_11 + m1.m_23 * m2.m_12 + m1.m_33 * m2.m_13,
                                m1.m_11 * m2.m_21 + m1.m_21 * m2.m_22 + m1.m_31 * m2.m_23,
                                m1.m_12 * m2.m_21 + m1.m_22 * m2.m_22 + m1.m_32 * m2.m_23,
                                m1.m_13 * m2.m_21 + m1.m_23 * m2.m_22 + m1.m_33 * m2.m_23,
                                m1.m_11 * m2.m_31 + m1.m_21 * m2.m_32 + m1.m_31 * m2.m_33,
                                m1.m_12 * m2.m_31 + m1.m_22 * m2.m_32 + m1.m_32 * m2.m_33,
                                m1.m_13 * m2.m_31 + m1.m_23 * m2.m_32 + m1.m_33 * m2.m_33);
        }

        /// <summary>
        /// Liefert die Einheitsmatrix
        /// </summary>
        public static Matrix3D Identity
        {
            get { return new Matrix3D(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f); }
        }

        /// <summary>
        /// Translationsmatrix (Verschiebungsmatrix) berechnen
        /// </summary>
        /// <param name="pt">Verschiebevektor</param>
        /// <returns>Die Translationsmatrix</returns>
        public static Matrix3D Translation(PointF pt)
        {
            // Translationsmatrix berechnen
            return Translation(pt.X, pt.Y);
        }

        /// <summary>
        /// Translationsmatrix (Verschiebungsmatrix) berechnen
        /// </summary>
        /// <param name="pt">Verschiebevektor</param>
        /// <returns>Die Translationsmatrix</returns>
        public static Matrix3D Translation(Point pt) 
        {
            // Translationsmatrix berechnen
            return Translation(pt.X, pt.Y);
        }

        /// <summary>
        /// Translationsmatrix (Verschiebungsmatrix) berechnen
        /// </summary>
        /// <param name="x">X-Komponente des Verschiebevektors</param>
        /// <param name="y">Y-Komponente des Verschiebevektors</param>
        /// <returns>Die Translationsmatrix</returns>
        public static Matrix3D Translation(float x, float y) 
        {
            // Translationsmatrix berechnen
            return new Matrix3D(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, x, y, 1.0f);
        }

        /// <summary>
        /// Skalierungsmatrix berechnen
        /// </summary>
        /// <param name="x">X-Komponente des Skalierungswertes</param>
        /// <param name="y">Y-Komponente des Skalierungswertes</param>
        /// <returns>Sie Skalierungsmatrix</returns>
        public static Matrix3D Scaling(float x, float y) 
        {
            return new Matrix3D(x, 0.0f, 0.0f, 0.0f, y,  0.0f, 0.0f, 0.0f, 1.0f);   
        }

        /// <summary>
        /// Determinante berechnen
        /// </summary>
        /// <returns>Die Determinante</returns>
        public float Determinant 
        {
            get
            {
                return m_11 * (m_22 * m_33 - m_23 * m_32) -
                       m_12 * (m_21 * m_33 - m_23 * m_31) +
                       m_13 * (m_21 * m_32 - m_22 * m_31);
            }
        }

        /// <summary>
        /// Invertierte (umgekehrte) Matrix berechnen
        /// </summary>
        /// <returns></returns>
        public Matrix3D Invert
        {
            get
            {
                float fInvDet = Determinant;

                if (fInvDet == 0.0f) return Matrix3D.Identity;
                fInvDet = 1.0f / fInvDet;

                // Invertierte Matrix berechnen
                Matrix3D mResult = new Matrix3D();

                mResult.M11 = fInvDet * (M22 * M33 - M23 * M32);
                mResult.M12 = -fInvDet * (M12 * M33 - M13 * M32);
                mResult.M13 = fInvDet * (M12 * M23 - M13 * M22);
                mResult.M21 = -fInvDet * (M21 * M33 - M23 * M31);
                mResult.M22 = fInvDet * (M11 * M33 - M13 * M31);
                mResult.M23 = -fInvDet * (M11 * M23 - M13 * M21);
                mResult.M31 = fInvDet * (M21 * M32 - M22 * M31);
                mResult.M32 = -fInvDet * (M11 * M32 - M12 * M31);
                mResult.M33 = fInvDet * (M11 * M22 - M12 * M21);

                return mResult;
            }
        }

        /// <summary>
        /// Transponierte Matrix berechnen
        /// </summary>
        /// <returns>Die transponierte Matrix</returns>
        public Matrix3D Transpose() 
        {
            // Matrix transponieren
            return new Matrix3D(m_11, m_21, m_31, m_12, m_22, m_32, m_13, m_23, m_33);
        }

        /// <summary>
        /// Transformiere einen Vektor
        /// </summary>
        /// <param name="p">Der zu transfomierende Punkt</param>
        /// <returns>Der transformierte Punkt</returns>
        public PointF Transform(PointF p) 
        {
            float x = p.X * m_11 + p.Y * m_21 + m_31;
            float y = p.X * m_12 + p.Y * m_22 + m_32;

            return new PointF(x, y);
        }

        /// <summary>
        /// Transformiere einen Vektor
        /// </summary>
        /// <param name="p">Der zu transfomierende Punkt</param>
        /// <returns>Der transformierte Punkt</returns>
        public Point Transform(Point p)
        {
            float x = p.X * m_11 + p.Y * m_21 + m_31;
            float y = p.X * m_12 + p.Y * m_22 + m_32;

            return new Point((int)x, (int)y);
        }

        /// <summary>
        /// Vergleicht die aktuelle Matrix mit m 
        /// </summary>
        /// <param name="m">Die zu vergleichende Matrix</param>
        /// <returns>true wenn Gleichheit, false sonst</returns>
        public bool Equals(Matrix3D m)
        {
            return (m_11 == m.m_11 && m_12 == m.m_12 && m_13 == m.m_13 &&
                    m_21 == m.m_21 && m_22 == m.m_22 && m_23 == m.m_23 &&
                    m_31 == m.m_31 && m_32 == m.m_32 && m_33 == m.m_33);
        }

        /// <summary>
        /// In Stringrepräsentation konvertieren
        /// </summary>
        /// <returns>Die Stringrepräsentation der Matrix</returns>
        public override string ToString()
        {
            return "< " + m_11 + ", " + m_12 + ", " + m_13 + ">, <" + m_21 + ", " + m_22 + ", " + m_23 + ">, <" + m_31 + ", " + m_32 + ", " + m_33 + ">";
        }

        /// <summary>
        /// Liefert oder setzt m11
        /// </summary>
        [XmlAttribute("m11")]
        public float M11 
        {
            get { return m_11; }
            set { m_11 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m12
        /// </summary>
        [XmlAttribute("m12")]
        public float M12
        {
            get { return m_12; }
            set { m_12 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m13
        /// </summary>
        [XmlAttribute("m13")]
        public float M13
        {
            get { return m_13; }
            set { m_13 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m21
        /// </summary>
        [XmlAttribute("m21")]
        public float M21
        {
            get { return m_21; }
            set { m_21 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m12
        /// </summary>
        [XmlAttribute("m22")]
        public float M22
        {
            get { return m_22; }
            set { m_22 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m13
        /// </summary>
        [XmlAttribute("m23")]
        public float M23
        {
            get { return m_23; }
            set { m_23 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m31
        /// </summary>
        [XmlAttribute("m31")]
        public float M31
        {
            get { return m_31; }
            set { m_31 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m32
        /// </summary>
        [XmlAttribute("m32")]
        public float M32
        {
            get { return m_32; }
            set { m_32 = value; }
        }

        /// <summary>
        /// Liefert oder setzt m33
        /// </summary>
        [XmlAttribute("m33")]
        public float M33
        {
            get { return m_33; }
            set { m_33 = value; }
        }
    }
}
