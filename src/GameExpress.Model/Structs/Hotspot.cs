using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Hotspot
    /// </summary>
    [XmlType("hotspot")]
    public struct Hotspot
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
        public Hotspot(string value)
        {
            var split = value.Replace("(", "").Replace(")", "").Split(',');

            X = Convert.ToByte(split[0].Trim());
            Y = Convert.ToByte(split[1].Trim());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Hotspot(int value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert der X- und Y-Koordinate</param>
        public Hotspot(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="hotspot">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        static public implicit operator Point(Hotspot hotspot)
        {
            return new Point(hotspot.X, hotspot.Y);
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="point">Der Hotspot</param>
        /// <returns>Der umgewandelte Hotspot</returns>
        static public implicit operator Hotspot(Point point)
        {
            return new Hotspot((int)point.X, (int)point.Y);
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return "(" + X.ToString() + "," + Y.ToString() + ")";
        }
    }
}
