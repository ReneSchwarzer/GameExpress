using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    public class Tweening
    {
        /// <summary>
        /// Tweening ein / aus
        /// </summary>
        [XmlAttribute("enable")]
        public bool Enable { get; set; }
    }
}
