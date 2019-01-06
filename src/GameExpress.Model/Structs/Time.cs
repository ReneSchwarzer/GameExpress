using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    [XmlType("time")]
    public class Time
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Time()
        {
            Ticks = 0;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="ticks">Die Ticks</param>
        public Time(Time ticks)
        {
            Ticks = ticks != null ? Ticks : 0;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ticks">Die Ticks</param>
        public Time(ulong ticks)
        {
            Ticks = ticks;
        }

        /// <summary>
        /// Nach einer absichtlichen Zeitsteuerungsdiskontinuität (z. B. ein blockierender EA-Vorgang)
        /// Dies aufrufen, um zu vermeiden, dass die feste Zeitschrittlogik eine Folge von aufholenden Aktualisierungsaufrufen versucht.
        /// </summary>
        public void ResetElapsedTime()
        {
            Ticks = 0;
        }

        /// <summary>
        /// Erhöt den aktuellen Tick
        /// </summary>
        public void AddTick()
        {
            Ticks++;
        }

        /// <summary>
        /// Erhöt den aktuellen Tick
        /// </summary>
        /// <param name="ticks">Die Ticks</param>
        public void AddTick(ulong ticks)
        {
            Ticks += ticks;
        }

        /// <summary>
        /// Zeitgeberzustand aktualisieren, dabei die angegebene Aktualisierungsfunktion entsprechend viele Male aufrufen.
        /// </summary>
        public ulong Ticks { get; set; }

        /// <summary>
        /// Kopieroperator
        /// </summary>
        /// <param name="time">Das zu konvertierende Zeitobjekt</param>
        public static explicit operator ulong(Time time)
        {
            return time.Ticks;
        }

        /// <summary>
        /// Wandelt das Objekt in einen String um
        /// </summary>
        /// <returns>Das Objekt in Stringrepräsentation</returns>
        public override string ToString()
        {
            return Ticks + " Ticks";
        }
    }
}
