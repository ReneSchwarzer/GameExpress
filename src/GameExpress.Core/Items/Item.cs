using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using GameExpress.Core.Structs;

namespace GameExpress.Core.Items
{
    [DefaultPropertyAttribute("Name")]
    public abstract class Item : Tree<Item>, IItem
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich Namenseigenschaften geändert haben
        /// </summary>
        public event EventHandler<ItemEventArgs> NameChanged;

        /// <summary>
        /// Event zum Mitteilen, dass sich Eigenschaften geändert haben
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Der Name des Items 
        /// </summary>
        private string m_name;

        /// <summary>
        /// Die Beschreibung
        /// </summary>
        private string m_note;

        /// <summary>
        /// Die Sichtbarbeit/ Nutzbarkeit
        /// </summary>
        private bool m_enable;

        /// <summary>
        /// Bearbeitungsspeere
        /// </summary>
        private bool m_lock;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        /// <param name="autoGUID">Bestimmt ob die ID automatisch ermittelt werden soll</param>
        public Item(ItemContext context, bool autoGUID)
        {
            Context = context;
            m_name = Context != null ? Context.Name : "";
            GUID = autoGUID ? ItemContext.NextGUID() : 0;
            
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
        public virtual void Update(UpdateContext uc)
        {

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void Presentation(PresentationContext pc)
        {

        }

        /// <summary>
        /// Sucht ein Item anahnd des Namens
        /// </summary>
        /// <param name="name">Der Name des gesuchten Items</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        public IItem FindItem(string name, bool oneLevel = false)
        {
            var list = new List<IItem>
            (
                oneLevel ? Children.Select(x => x as IItem) : GetPreOrder().Select(x => x as IItem)
            );

            return list.Where(x => (x as Item).Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() as Item;
        }

        /// <summary>
        /// Sucht ein Item anahnd verschiedener Parameter
        /// </summary>
        /// <param name="type">Der Typ des gesuchten Items</param>
        /// <param name="name">Der Name des gesuchten Items</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        public IEnumerable<T> FindItem<T>(string name = null, bool oneLevel = false) where T : Item
        {
            if (string.IsNullOrWhiteSpace(name) && oneLevel)
            {
                return Children.Where(x => x.GetType().Equals(typeof(T))).Select(x => x as T);
            }
            else if (!string.IsNullOrWhiteSpace(name) && oneLevel)
            {
                return Children.Where
                (
                    x => x.GetType().Equals(typeof(T)) &&
                    (x as Item).Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                ).Select
                (
                    x => x as T
                );
            }
            else if (string.IsNullOrWhiteSpace(name) && !oneLevel)
            {
                return GetPreOrder().Where(x => x.GetType().Equals(typeof(T))).Select(x => x as T);
            }
            else if (!string.IsNullOrWhiteSpace(name) && !oneLevel)
            {
                return GetPreOrder().Where
                (
                    x => x.GetType().Equals(typeof(T)) &&
                    (x as Item).Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                ).Select
                (
                    x => x as T
                );
            }

            return new List<T>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public virtual IItem Copy()
        {
            var copy = Context.ItemFactory();
            copy.Name = Name;
            copy.Note = Note;
            copy.Enable = Enable;
            copy.Lock = Lock;

            return copy;
        }

        /// <summary>
        /// Das Kontextobjekt
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlIgnore]
        public ItemContext Context { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Name des Items
        /// </summary>
        [Category("Allgmein"), Description("Geben Sie hier einen Namen für das Objekt an.")]
        [XmlAttribute("name")]
        public string Name
        {
            get { return m_name; }
            set
            {
                if (!m_name.Equals(value))
                {
                    m_name = value;
                    NameChanged?.Invoke(this, new ItemEventArgs(this));
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        [Category("Allgmein"), DisplayName("Beschreibung"), Description("Geben Sie hier eine Notiz an.")]
        [XmlElement("note")]
        public string Note
        {
            get { return m_note; }
            set
            {
                if ((m_note != null && !m_note.Equals(value)) || (m_note == null && value != null) )
                {
                    m_note = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert Informationen zu dem Item
        /// </summary>
        [Category("Allgmein"), Description("Liefert Informationen zu dem Objekt.")]
        [XmlIgnore]
        public string Info
        {
            get { return "ID=" + GUID; }
        }

        /// <summary>
        /// Liefert die eindeutige ID
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlAttribute("id")]
        public ulong GUID { get; set; }

        /// <summary>
        /// Blendet die Inszanz ein oder aus
        /// </summary>
        [Category("Ansicht"), Description("Blendet die Inszanz ein oder aus.")]
        [XmlAttribute("enable")]
        public bool Enable
        {
            get { return m_enable; }
            set
            {
                if (!m_enable.Equals(value))
                {
                    m_enable = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Speert die Instanz und schützt diese vor Eingaben
        /// </summary>
        [Category("Ansicht"), Description("Speert die Instanz und schützt diese vor Eingaben.")]
        [XmlAttribute("lock")]
        public bool Lock
        {
            get { return m_lock; }
            set
            {
                if (!m_lock.Equals(value))
                {
                    m_lock = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Löst das PropertyChanged-Event aus  
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Wandelt das Objekt in einen String um
        /// </summary>
        /// <returns>Das Objekt in Stringrepräsentation</returns>
        public override string ToString()
        {
            return Name + " " + Info;
        }
    }
}
