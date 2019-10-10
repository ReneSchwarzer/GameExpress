using GameExpress.Model.Structs;
using System.ComponentModel;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Basisklasse von Grafiken
    /// </summary>
    [XmlInclude(typeof(ItemScene))]
    [XmlInclude(typeof(ItemObject))]
    [XmlInclude(typeof(ItemImage))]
    [XmlType("graphics")]
    public abstract class ItemGraphics : ItemTreeNode, IItemVisual, IItemSizing, IItemHotSpot, IItemClickable
    {
        /// <summary>
        /// Der Hotspot
        /// </summary>
        private Hotspot m_hotspot = new Hotspot();

        /// <summary>
        /// Der Gammawert von 0-1
        /// </summary>
        private Gamma m_gamma;

        /// <summary>
        /// Der Alphawert von 0-255
        /// </summary>
        private Alpha m_alpha;

        /// <summary>
        /// Die Unschärfe von 0-255
        /// </summary>
        private Blur m_blur;

        /// <summary>
        /// Der Farbton
        /// </summary>
        private Hue m_hue;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemGraphics()
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            pc.AddAlpha(m_alpha);
            pc.AddBlur(m_blur);
            pc.AddGamma(m_gamma);
            pc.AddHue(m_hue);
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public abstract Item HitTest(HitTestContext hc, Vector point);

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public abstract Matrix3D GetMatrix();

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemGraphics;
            copy.Hotspot = Hotspot;
            copy.Gamma = Gamma;
            copy.Alpha = Alpha;
            copy.Hue = Hue;

            return copy as T;
        }

        /// <summary>
        /// Wird aufgerufen, wennsich der x-Wert oder y-Wert innerhalb des Hotspot ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnHotspotPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged("Hotspot");
        }

        /// <summary>
        /// Liefert oder setzt den HotSpot
        /// </summary>
        [XmlElement("hotspot")]
        public Hotspot Hotspot
        {
            get => m_hotspot;
            set
            {
                if (!m_hotspot.Equals(value))
                {
                    m_hotspot.PropertyChanged -= OnHotspotPropertyChanged;

                    m_hotspot = value;

                    m_hotspot.PropertyChanged += OnHotspotPropertyChanged;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Gamma-Wert
        /// </summary>
        [XmlElement("gamma")]
        public Gamma Gamma
        {
            get => m_gamma;
            set
            {
                if (!m_gamma.Equals(value))
                {
                    m_gamma = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Alphawert
        /// </summary>
        [XmlElement("alpha")]
        public Alpha Alpha
        {
            get => m_alpha;
            set
            {
                if (!m_alpha.Equals(value))
                {
                    m_alpha = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Unschärfewert
        /// </summary>
        [XmlElement("blur")]
        public Blur Blur
        {
            get => m_blur;
            set
            {
                if (!m_blur.Equals(value))
                {
                    m_blur = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Farbton-Eigenschaft
        /// </summary>
        [XmlIgnore]
        public Hue Hue
        {
            get => m_hue;
            set
            {
                if (!m_hue.Equals(value))
                {
                    m_hue = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public abstract Vector Size { get; }
    }
}
