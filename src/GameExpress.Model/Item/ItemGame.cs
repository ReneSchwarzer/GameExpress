using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlRoot(ElementName = "game")]
    public class ItemGame : ItemTreeNode, IItemVisual
    {
        /// <summary>
        /// Die Weite des Spielbereiches
        /// </summary>
        private int m_width = 1024;

        /// <summary>
        /// Die Höhe des Spielbereiches
        /// </summary>
        private int m_height = 768;

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

        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public virtual Matrix3D GetMatrix()
        {
            return Matrix3D.Identity;
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
        /// Lädt die Ressourcen des Items
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {
            foreach (var c in Children)
            {
                c.CreateResources(g);
            };

        }

        /// <summary>
        /// Liefert oder setzt einen Verweis auf das aktuelle Projekt
        /// </summary>
        [XmlIgnore]
        public new Project Project { get; set; }

        /// <summary>
        /// Liefert oser setzt die Weite des Spielbereiches
        /// </summary>
        [XmlAttribute("width")]
        public int Width
        {
            get => m_width;
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
            get => m_height;
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
        public virtual Size Size => new Size(Width, Height);
    }
}
