using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace GameExpress.Core.Structs
{
    /// <summary>
    /// Der Kontext, indem ein Update der GameLoop ausgefürt wird
    /// </summary>
    public class UpdateContext
    {
	    /// <summary>
	    /// Konstruktor
	    /// </summary>
	    public UpdateContext()
        {
            Time = new Structs.Time();
            Level = 1;
        }
	    
        /// <summary>
        /// Kopier - Konstruktor
        /// </summary>
        /// <param name="pc">Der Presentation Kontext</param>
	    public UpdateContext(UpdateContext pc)
            :this()
        {
            Designer = pc.Designer;
            Level = pc.Level+1;
            Time = pc.Time;
        }
            	
        /// <summary>
        /// Der updatekontext wird im Designer ausgeführt
        /// </summary>
        public bool Designer { get; set; }

        /// <summary>
        /// Tiefe
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        public Time Time { get; set; }
    }
}
