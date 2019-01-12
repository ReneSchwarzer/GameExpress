using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Hotspot
    /// </summary>
    [XmlType("hotspot")]
    public class Hotspot : INotifyPropertyChanged
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich eine Eigenschaften geändert hat
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Die X-Koordinate
        /// </summary>
        private int m_x = 0;

        /// <summary>
        /// Die Y-Koordinate
        /// </summary>
        private int m_y = 0;

        /// <summary>
        /// Die X-Koordinate
        /// </summary>
        [XmlAttribute("x")]
        public int X
        {
            get { return m_x; }
            set
            {
                if (!m_x.Equals(value))
                {
                    m_x = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die Y-Koordinate
        /// </summary>
        [XmlAttribute("y")]
        public int Y
        {
            get { return m_y; }
            set
            {
                if (!m_y.Equals(value))
                {
                    m_y = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Hotspot()
            :this(0)
        {
        }

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

        /// <summary>
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
