using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using GameExpress.Core.Structs;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    public abstract class ItemVisualAnimated : ItemVisualInstanceContainer
    {
        /// <summary>
        /// Liefert oder setzt die Zeit, bei dem die Annimation beendet wird
        /// </summary>
        ulong? m_endTime;
        [Category("Annimation"), DisplayName("Endzeit"), Description("Liefert oder setzt die Endzeit der Annimation")]
        [XmlElement("endtime")]
        public ulong? EndTime
        {
            get { return m_endTime; }
            set
            {
                if (m_endTime != value)
                {
                    m_endTime = value;

                    var attributes = GetType()?.GetProperty("Loop")?.GetCustomAttributes(typeof(BrowsableAttribute), false);
                    attributes.SetValue(new BrowsableAttribute(m_endTime != null), 0);

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt ob die Annimation in einer Schleife wiederholt werden soll
        /// </summary>
        [Browsable(true), Category("Annimation"), DisplayName("Schleife"), Description("Liefert oder setzt ob die Annimation in einer Schleife wiederholt werden soll")]
        [XmlAttribute("loop")]
        public Loop Loop { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualAnimated(ItemContext context, bool autoGUID)
            :base(context, autoGUID)
        {
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(new PresentationContext(pc)
            {
                Time = LocalTime(pc.Time),
                Level = pc.Level
            });
        }

        /// <summary>
        /// Berechnet die lokale Zeit
        /// </summary>
        /// <param name="time">Die globale Zeit</param>
        /// <returns>Die interne Zeit</returns>
        public Time LocalTime(Time time)
        {
            var local = new Time();

            if (EndTime.HasValue && Loop == Loop.None &&  (ulong)time > EndTime.Value)
            {
                // Zeit begrenzen
                local.AddTick(EndTime.Value);
            }
            else if (EndTime.HasValue && Loop == Loop.Default)
            {
                // Schleife
                local.AddTick((ulong)time % (ulong)EndTime.Value);
            }
            else if (EndTime.HasValue && Loop == Loop.Oscillate)
            {
                // Schwingen
                var direction = ((ulong)time / (ulong)EndTime.Value) % 2;
                switch (direction)
                {
                    case 0:
                        local.AddTick((ulong)time % (ulong)EndTime.Value);
                        break;
                    default:
                        local.AddTick((ulong)EndTime.Value - (ulong)time % (ulong)EndTime.Value);
                        break;
                }
                
            }
            else
            {
                local.AddTick((ulong)time);
            }

            return local;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualAnimated;
            copy.EndTime = EndTime;
            copy.Loop = Loop;
            
            return copy;
        }

        /// <summary>
        /// Wandelt das Objekt in einen String um
        /// </summary>
        /// <returns>Das Objekt in Stringrepräsentation</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
