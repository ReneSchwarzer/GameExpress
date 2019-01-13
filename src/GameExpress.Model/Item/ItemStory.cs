using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Objektinstanz
    /// </summary>
    [XmlType("story")]
    public class ItemStory : ItemVisual
    {
        /// <summary>
        /// Das Item
        /// </summary>
        private string m_item;

        /// <summary>
        /// Liefert oder setzt die Schlüselbilder
        /// </summary>
        [XmlElement("keyframe")]
        public ObservableCollection<ItemKeyFrame> KeyFrames { get; set; } = new ObservableCollection<ItemKeyFrame>();

        /// <summary>
        /// Liefert oder setzt den Verweis auf das zugehörige Objekt
        /// </summary>
        [XmlIgnore]
        public ItemAnimation Object { get { return Parent as ItemAnimation; } }

        /// <summary>
        /// Die ID des mit der Instanz verbundenen Element
        /// </summary>
        [XmlIgnore]
        public Item Instance { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemStory()
        {

        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            //KeyFrames.CollectionChanged += (s, e) =>
            //{
            //    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            //    {
            //        foreach (Item v in e.NewItems)
            //        {
            //            //AddChild(v);
            //        }
            //    }
            //};
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

            if (Instance == null)
            {
                AttachedInstance(Item);
            }

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (pc.Level > 10) return;

            base.Presentation(pc);

            if (Instance == null)
            {
                AttachedInstance(Item);
            }

            var currentKeyFrame = GetKeyFrame((ulong)pc.Time);

            if (currentKeyFrame != null)
            {
                var newPC = new PresentationContext(pc) { Level = pc.Level };
                newPC.Matrix *= currentKeyFrame.Matrix;

                if (Instance is ItemGraphics)
                {
                    var graphics = Instance as ItemGraphics;
                    newPC.Matrix *= Matrix3D.Translation(graphics.Hotspot.X * -1, graphics.Hotspot.Y * -1);
                }

                Instance?.Presentation(newPC);
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemStory;

            return copy as T;
        }

        /// <summary>
        /// Ermittelt und fügt die Instanz hinzu
        /// </summary>
        /// <param name="item">Das Item</param>
        protected void AttachedInstance(string item)
        {
            if (Object == null) return;

            // Suche Item
            var orgin = Object.Root.FindItem(Item);

            if (orgin != null)
            {
                //Instance = orgin.Copy();
                //Instance.Name = Name + "_" + Instance.Name;
                //(Instance as Item).Parent = this;

                Instance = orgin;
            }
        }

        /// <summary>
        /// Liefert ein Schlüselbild zu der gegebenen Zeit
        /// </summary>
        /// <param name="time">Die Zeit</param>
        /// <returns>Das Schlüsselbid oder null</returns>
        public ItemKeyFrameBase GetKeyFrame(ulong time)
        {
            ItemKeyFrame prevKeyFrame = null;
            ItemKeyFrame nextKeyFrame = null;

            foreach (var k in KeyFrames)
            {
                if (k.From <= time && time <= k.From + k.Duration)
                {
                    return k;
                }
                else if (k.From + k.Duration < time)
                {
                    prevKeyFrame = k;
                }
                else if (time < k.From + k.Duration)
                {
                    nextKeyFrame = k;

                    break;
                }
            }

            // Tweening
            if (prevKeyFrame != null && nextKeyFrame != null && prevKeyFrame.Tweening)
            {
                ulong from = prevKeyFrame.From + prevKeyFrame.Duration;
                ulong till = nextKeyFrame.From;

                Matrix3D mt = prevKeyFrame.Matrix.Invert * nextKeyFrame.Matrix;

                float t = (time - from) / (float)(till - from); //in %

                return new ItemKeyFrameTweening()
                {
                    //From = from,
                    //Duration = till - from,
                    Matrix = prevKeyFrame.Matrix * new Matrix3D
                    (
                        (mt.M11 - 1) * t + 1, mt.M12 * t, 0,
                        mt.M21 * t, (mt.M22 - 1) * t + 1, 0,
                        mt.M31 * t, mt.M32 * t, 1
                    )
                    // ToDo: Alpha, usw.
                };
            }

            return null;
        }

        /// <summary>
        /// Liefert oder setzt das mit der Instanz verbundene Element
        /// </summary>
        [XmlAttribute("item")]
        public string Item
        {
            get { return m_item; }
            set
            {
                if (m_item == null ? true : !m_item.Equals(value))
                {
                    m_item = value;
                    AttachedInstance(Item);

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
                return new Size();
            }
        }
    }
}

