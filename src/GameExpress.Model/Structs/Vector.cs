using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int X { get; set; }

        /// <summary>
        /// Die Y-Koordinate
        /// </summary>
        [XmlAttribute("y")]
        public int Y { get; set; }

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
        public Vector(int value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="hotspot">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        static public implicit operator Point(Vector hotspot)
        {
            return new Point(hotspot.X, hotspot.Y);
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="point">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        static public implicit operator Vector(Point point)
        {
            return new Vector((int)point.X, (int)point.Y);
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return "<" + X.ToString() + "," + Y.ToString() + ">";
        }
    }
}
