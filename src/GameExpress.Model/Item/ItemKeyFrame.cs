using GameExpress.Model.Structs;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    [XmlType("keyframebase")]
    [XmlInclude(typeof(ItemKeyFrameAct))]
    [XmlInclude(typeof(ItemKeyFrameTweening))]
    public abstract class ItemKeyFrame : Item
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf die zugehörige Story
        /// </summary>
        [XmlIgnore]
        public ItemStory Story { get; set; }

        /// <summary>ItemKeyFrameBase
        /// Konstruktor
        /// </summary>
        public ItemKeyFrame()
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
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemKeyFrame;
            copy.Story = Story;
            return copy as T;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return "KeyFrame";
        }
    }
}