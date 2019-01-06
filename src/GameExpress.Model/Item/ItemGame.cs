using Microsoft.Graphics.Canvas;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlRoot(ElementName = "game")]
    public class ItemGame : ItemVisual
    {
        /// <summary>
        /// Die Weite des Spielbereiches
        /// </summary>
        private int m_width;

        /// <summary>
        /// Die Höhe des Spielbereiches
        /// </summary>
        private int m_height;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemGame()
        {
        }

        /// <summary>
        /// Initialisiert das Spiel
        /// </summary>
        public override void Init()
        {
            Width = 1024;
            Height = 768;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemGame;
            copy.Height = Height;
            copy.Width = Width;

            return copy as T;
        }

        /// <summary>
        /// Liefert oser setzt die Weite des Spielbereiches
        /// </summary>
        [XmlAttribute("width")]
        public int Width
        {
            get { return m_width; }
            set
            {
                if (!m_width.Equals(value))
                {
                    m_width = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oser setzt die Höhe des Spielbereiches
        /// </summary>
        [XmlAttribute("height")]
        public int Height
        {
            get { return m_height; }
            set
            {
                if (!m_height.Equals(value))
                {
                    m_height = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public override Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
        }

        /// <summary>
        /// Lädt die Ressourcen des Items
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {
            Parallel.ForEach(Children, (v) => 
            {
                v.CreateResources(g);
            });

        }
    }
}
