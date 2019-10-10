using GameExpress.Model.Structs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Verwaltung einer Instanz
    /// </summary>
    public class ItemInstance : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich eine Eigenschaften geändert hat
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Die ID 
        /// </summary>
        private string m_id = string.Empty;

        /// <summary>
        /// Der Name des Items 
        /// </summary>
        private string m_name = string.Empty;

        /// <summary>
        /// Liefert oder setzt die Instanz
        /// </summary>
        private Item m_instance;

        /// <summary>
        /// Die ID des ausgewählten Zustandes
        /// </summary>
        private string m_stateID = string.Empty;

        /// <summary>
        /// Der NAme des ausgewählten Zustandes
        /// </summary>
        private string m_stateName = string.Empty;

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        private Item m_state;

        /// <summary>
        /// Liefert oder setzt den Name des Items
        /// </summary>
        [XmlAttribute("id")]
        public string ID
        {
            get => m_id;
            set
            {
                if (!m_id.Equals(value))
                {
                    m_id = value;
                    StateID = string.Empty;
                    Name = string.Empty;
                    Instance = null;

                    RaisePropertyChanged();
                    AttachedInstance();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Name des Items
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get => m_name;
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
        /// Liefert oder setzt die ID des ausgewählten Zustandes
        /// </summary>
        [XmlAttribute("stateid")]
        public string StateID
        {
            get => m_stateID;
            set
            {
                if (!m_stateID.Equals(value))
                {
                    m_stateID = value;
                    StateName = string.Empty;
                    State = null;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Namen des ausgewählten Zustandes
        /// </summary>
        [XmlAttribute("state")]
        public string StateName
        {
            get => m_stateName;
            set
            {
                if (!m_stateName.Equals(value))
                {
                    m_stateName = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt das mit der Instanz verbundene Element
        /// </summary>
        [XmlIgnore]
        public Item Instance
        {
            get => m_instance;
            set
            {
                if (m_instance != value)
                {
                    m_instance = value;
                    RaisePropertyChanged();
                }

                RaisePropertyChanged("States");
                RaisePropertyChanged("HasState");
                RaisePropertyChanged("Size");
            }
        }

        /// <summary>
        /// Liefert oder setzt den Zustand, welcher mit der Instanz verbundene ist
        /// </summary>
        [XmlIgnore]
        public Item State
        {
            get => m_state;
            set
            {
                if (m_state != value)
                {
                    m_state = value;
                    RaisePropertyChanged();

                    RaisePropertyChanged("Size");
                }
            }
        }

        /// <summary>
        /// Liefert ob Instanzzustände vorhanden sind
        /// </summary>
        [XmlIgnore]
        public bool HasState => States != null && States.Count > 0;

        /// <summary>
        /// Liefert ob die Instanzzustände
        /// </summary>
        [XmlIgnore]
        public ObservableCollection<IItemState> States => (Instance as IItemStates)?.States;

        /// <summary>
        /// Liefert den Elternknoten
        /// </summary>
        [XmlIgnore]
        public ItemTreeNode Parent { get; set; }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public Vector Size
        {
            get
            {
                var item = State ?? Instance;

                if (item is IItemSizing sizing)
                {
                    return sizing.Size;
                }

                return new Vector();
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemInstance()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="parent">Der übergeordnete Knoten</param>
        public ItemInstance(ItemTreeNode parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Init()
        {
            AttachedInstance();
        }

        /// <summary>
        /// Entfernen nicht mehr benötigter Ressourcen des Items
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public virtual void Update(UpdateContext uc)
        {
            if (Instance == null)
            {
                AttachedInstance();
            }

            if (!string.IsNullOrWhiteSpace(StateID))
            {
                if (State == null)
                {
                    AttachedStateInstance();
                }

                State?.Update(uc);
                return;

            }

            Instance?.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void Presentation(PresentationContext pc)
        {
            if (Instance is IItemClipping clipping && clipping .Clipping)
            {
                var origin = pc.Matrix.Transform(new Vector());
                var size = pc.Matrix.Transform(Size);
                var rect = new Rect(origin.X, origin.Y, size.X - origin.X, size.Y - origin.Y);

                using (pc.Graphics.CreateLayer(1f, rect))
                {
                    if (State != null)
                    {
                        State?.Presentation(pc);
                    }
                    else
                    {
                        Instance?.Presentation(pc);
                    }
                }
            }
            else
            {
                if (State != null)
                {
                    State?.Presentation(pc);
                }
                else
                {
                    Instance?.Presentation(pc);
                }
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public virtual ItemInstance Copy()
        {
            var copy = new ItemInstance();
            copy.ID = ID;
            copy.StateID = StateID;
            copy.AttachedInstance();
            copy.AttachedStateInstance();

            return copy;
        }

        /// <summary>
        /// Ermittelt und fügt die Instanz hinzu
        /// </summary>
        private void AttachedInstance()
        {
            if (!string.IsNullOrWhiteSpace(ID))
            {
                // Suche Item
                var origin = Parent?.Root.FindId(ID);

                if (origin is Item item)
                {
                    Instance = item.Copy() as Item;
                    Name = Instance?.Name;
                }
            }
        }

        /// <summary>
        /// Ermittelt und fügt die Statusinstanz  hinzu
        /// </summary>
        private void AttachedStateInstance()
        {
            if (!string.IsNullOrWhiteSpace(StateID))
            {
                // Suche Item
                var origin = Parent?.Root.FindId(StateID);

                if (origin is Item item)
                {
                    State = item.Copy() as Item;
                    StateName = State?.Name;
                }
            }
        }

        /// <summary>
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return GetType().Name + " " + Name;
        }
    }
}
