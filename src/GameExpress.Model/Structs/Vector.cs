using System;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Zweidimensionaler Vektor
    /// </summary>
    [XmlType("vector")]
    public struct Vector
    {
        /// <summary>
        /// Die X-Koordinate
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; }

        /// <summary>
        /// Die Y-Koordinate
        /// </summary>
        [XmlAttribute("y")]
        public double Y { get; set; }

        /// <summary>
        /// Liefert den Betrag (Norm) des Vektors
        /// </summary>
        public double Length => Math.Sqrt(Math.Pow(X, 2f) + Math.Pow(Y, 2f));

        /// <summary>
        /// Liefert den einen der beiden möglichen Normalenvektor 
        /// Geliefert wird für den Richtungsvektor <![CDATA[<a,b>]]> der Normalenvektor <![CDATA[<b,-a>]]>.
        /// Nicht geliefert wird für  den Richtungsvektor <![CDATA[<a,b>]]> der Normalenvektor <![CDATA[<-b,a>]]>.
        /// </summary>
        public Vector Normal => new Vector(Y, -X);

        /// <summary>
        /// Liefert den Einheitsvektor 
        /// </summary>
        public Vector Unit => this / Length;

        /// <summary>
        /// Liefert einen Vektor, der einen ungültigen Wert enthällt
        /// </summary>
        public static Vector Invalid => new Vector(double.NaN);

        /// <summary>
        /// Liefert einen Vektor, der einen unendlichen Wert enthällt
        /// </summary>
        public static Vector Infinity => new Vector(double.PositiveInfinity);

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert</param>
        public Vector(string value)
        {
            var split = value.Replace("<", "").Replace(">", "").Split(',');

            X = Convert.ToByte(split[0].Trim());
            Y = Convert.ToByte(split[1].Trim());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Vector(double value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Vector(int value)
            : this((double)value)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Vector(int x, int y)
            : this(x, (double)y)
        {
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="hotspot">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        public static implicit operator Point(Vector hotspot)
        {
            return new Point(hotspot.X, hotspot.Y);
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="point">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        public static implicit operator Vector(Point point)
        {
            return new Vector(point.X, point.Y);
        }

        /// <summary>
        /// Multiplikation zweier Vektoren
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator *(Vector v1, Vector v2)
        {
            return new Vector(v1.X * v2.X, v1.Y * v2.Y);
        }

        /// <summary>
        /// Multiplikation mit einem Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector operator *(Vector v, double scalar)
        {
            return new Vector(v.X * scalar, v.Y * scalar);
        }

        /// <summary>
        /// Multiplikation mit einem Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector operator *(Vector v, int scalar)
        {
            return new Vector(v.X * scalar, v.Y * scalar);
        }

        /// <summary>
        /// Division mit einem Skalar
        /// </summary>
        /// <param name="v"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector operator /(Vector v, double scalar)
        {
            return new Vector(v.X / scalar, v.Y / scalar);
        }

        /// <summary>
        /// Addition zweier Vektoren
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        /// <summary>
        /// Subtraktion zweier Vektoren
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Gleichheit zweier Vektoren
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>true wenn beide Vektoren gleich sind, false sonst</returns>
        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        /// <summary>
        /// Ungelichheit zweier Vektoren
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns>true wenn beide Vektoren ungelich sind, false sonst</returns>
        public static bool operator !=(Vector v1, Vector v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return "<" + X.ToString() + "; " + Y.ToString() + ">";
        }
    }
}
