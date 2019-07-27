using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Unschärfe
    /// </summary>
    [XmlType("blur")]
    public struct Blur
    {
        /// <summary>
        /// Die Value-Eigenschaft
        /// </summary>
        [XmlAttribute("value")]
        public byte Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="blur">Der Unschärfewert</param>
        public Blur(byte blur)
        {
            Value = blur;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="blur">Der Unschärfewert</param>
        /// <returns>Der umgewandelte Unschärfewert</returns>
        public static implicit operator byte(Blur blur)
        {
            return blur.Value;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="blur">Der Unschärfewert</param>
        /// <returns>Der umgewandelte Unschärfewert</returns>
        public static implicit operator Blur(byte blur)
        {
            return new Blur(blur);
        }

        /// <summary>
        /// Alphawert hinzufügen
        /// </summary>
        /// <param name="blur">Der Unschärfewert</param>
        public void Add(Blur blur)
        {
            var f = Value + ((255.0f - (this)) * (blur / 255.0f));
            if (f > 255)
            {
                f = 255;
            }

            Value = (byte)f;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
