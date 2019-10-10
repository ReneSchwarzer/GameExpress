using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("tweening")]
    public class ItemKeyFrameTweening : ItemKeyFrame, IItemVisual, IItemSizing, IItemClickable
    {
        /// <summary>
        /// Liefert oder setzt den Vorgänger
        /// </summary>
        [XmlIgnore]
        public ItemKeyFrameAct Predecessor { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Nachfolger
        /// </summary>
        [XmlIgnore]
        public ItemKeyFrameAct Successor { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Tweeningfortschritt in %
        /// </summary>
        [XmlIgnore]
        public float Progress { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemKeyFrameTweening()
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        /// <param name="predecessor">Der Vorgängerframe</param>
        /// <param name="successor">Der Nachfolgeframe</param>
        /// <param name="progress">Der Fortschritt in %</param>
        /// 
        public virtual void Init(ItemKeyFrameAct predecessor, ItemKeyFrameAct successor, float progress)
        {
            Predecessor = predecessor;
            Successor = successor;
            Progress = progress;
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            uc.Matrix *= GetMatrix();

            base.Update(uc);

            Story?.Instance?.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            pc.Matrix *= GetMatrix();

            base.Presentation(pc);

            Story?.Instance?.Presentation(pc);
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public virtual Item HitTest(HitTestContext hc, Vector point)
        {
            hc.Matrix *= GetMatrix();

            if (Story.Instance is IItemClickable instance)
            {
                if (instance.HitTest(hc, point) != null)
                {
                    return Story;
                };
            }

            return null;
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public virtual Matrix3D GetMatrix()
        {
            var mt = Predecessor.GetMatrix().Invert * Successor.GetMatrix();

            return Predecessor.GetMatrix() * new Matrix3D
            (
                (mt.M11 - 1) * Progress + 1, mt.M12 * Progress, 0,
                mt.M21 * Progress, (mt.M22 - 1) * Progress + 1, 0,
                mt.M31 * Progress, mt.M32 * Progress, 1
            );
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemKeyFrameTweening>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemKeyFrameTweening;

            return copy as T;
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public virtual Vector Size
        {
            get
            {
                if (Story?.Instance?.Instance is IItemSizing instance)
                {
                    return instance.Size;
                }

                return new Vector();
            }
        }
    }
}