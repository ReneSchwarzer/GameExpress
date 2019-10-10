using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using System.Xml.Serialization;
using Windows.UI.Xaml;

namespace GameExpress.Model.Item
{
    [XmlType("sound")]
    public class ItemSound : ItemTreeNode
    {
        /// <summary>
        /// Die Soundquelle
        /// </summary>
        private string m_source = string.Empty;

        /// <summary>
        /// Liefert oder setzt den Soundplayer
        /// </summary>
        [XmlIgnore]
        public ElementSoundPlayer Player { get; set; }

        /// <summary>
        /// Liefert oder setzt die Soundquelle (Pfad+Dateiname)
        /// </summary>
        [XmlElement("source")]
        public string Source
        {
            get => m_source;
            set
            {
                m_source = value;
                Player = null;

                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemSound()
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {

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

        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemSound>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemSound;
            copy.Source = Source;

            return copy as T;
        }

        /// <summary>
        /// Lädt dden Sound aus der gegebenen Quelle
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {

        }
    }
}
