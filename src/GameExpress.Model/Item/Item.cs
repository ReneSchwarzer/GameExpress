using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    [XmlInclude(typeof(ItemTreeNode))]
    public abstract class Item : INotifyPropertyChanged
    {
        /// <summary>
        /// Der Name des Items 
        /// </summary>
        private string m_name = string.Empty;

        /// <summary>
        /// Die Beschreibung
        /// </summary>
        private string m_note = string.Empty;

        /// <summary>
        /// Die Sichtbarbeit/ Nutzbarkeit
        /// </summary>
        private bool m_enable = true;

        /// <summary>
        /// Bearbeitungsspeere
        /// </summary>
        private bool m_lock = false;

        /// <summary>
        /// Liefert oder setzt den Name des Items
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get { return m_name; }
            set
            {
                if (!m_name.Equals(value))
                {
                    m_name = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        [Category("Allgmein"), DisplayName("Beschreibung"), Description("Geben Sie hier eine Notiz an.")]
        [XmlElement("note", IsNullable = true)]
        public string Note
        {
            get { return m_note; }
            set
            {
                if ((m_note != null && !m_note.Equals(value)) || (m_note == null && value != null))
                {
                    m_note = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die eindeutige ID
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Blendet die Inszanz ein oder aus
        /// </summary>
        [XmlAttribute("enable")]
        public bool Enable
        {
            get { return m_enable; }
            set
            {
                if (!m_enable.Equals(value))
                {
                    m_enable = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Speert die Instanz und schützt diese vor Eingaben
        /// </summary>
        [XmlAttribute("lock")]
        public bool Lock
        {
            get { return m_lock; }
            set
            {
                if (!m_lock.Equals(value))
                {
                    m_lock = value;

                    RaisePropertyChanged();
                    RaisePropertyChanged("Unlock");
                }
            }
        }

        /// <summary>
        /// Prüft, ob Eingaben erlaubt sind
        /// </summary>
        [XmlIgnore]
        public bool Unlock
        {
            get { return !Lock; }
        }

        /// <summary>
        /// Event zum Mitteilen, dass sich eine Eigenschaften geändert hat
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Item()
        {
            Init();
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public virtual void Init()
        {
            m_enable = true;
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public abstract void Update(UpdateContext uc);

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public abstract void Presentation(PresentationContext pc);

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public virtual T Copy<T>() where T : Item, new()
        {
            var copy = new T()
            {
                Name = Name,
                Note = Note,
                Enable = Enable,
                Lock = Lock
            };

            return copy;
        }

        /// <summary>
        /// Lädt die Ressourcen des Items
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public virtual void CreateResources(ICanvasResourceCreator g)
        {
        }

        /// <summary>
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
