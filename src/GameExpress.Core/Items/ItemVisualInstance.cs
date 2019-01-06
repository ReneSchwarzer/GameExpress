using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using GameExpress.Core.Structs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    /// <summary>
    /// Objektinstanz
    /// </summary>
    [XmlType("instance")]
    public class ItemVisualInstance : ItemVisual
    {
        private string m_item;
        [NonSerialized]
        private Matrix3D m_matrix;
        [NonSerialized]
        private ObservableCollection<ItemVisualKeyFrame> m_keyFrames = new ObservableCollection<ItemVisualKeyFrame>();
        
        /// <summary>
        /// Liefert oder setzt den aktuellen KeyFrame
        /// </summary>
        private ItemVisualKeyFrame CurrentKeyFrame { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualInstance()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualInstance)))
        {
            
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualInstance(ItemContext context)
            : base(context, true)
        {
            Name = new ItemVisualInstanceContext().Name + "_" + GUID;
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            m_keyFrames.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (Item v in e.NewItems)
                    {
                        AddChild(v);
                    }
                }
            };
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
            base.Presentation(pc);

            if (Instance == null)
            {
                AttachedInstance(Item);
            }

            CurrentKeyFrame = GetKeyFrame((ulong)pc.Time);

            if (CurrentKeyFrame != null)
            {
                var newPC = new PresentationContext(pc);
                newPC.Matrix *= CurrentKeyFrame.Matrix;

                Instance?.Presentation(newPC);
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualInstance;
            copy.Item = Item;
            copy.Matrix = Matrix;

            foreach (var key in KeyFrames.Select(x => x.Copy() as ItemVisualKeyFrame))
            {
                copy.KeyFrames.Add(key);
            }

            return copy;
        }

        /// <summary>
        /// Ermittelt und fügt die Instanz hinzu
        /// </summary>
        /// <param name="item">Das Item</param>
        protected void AttachedInstance(string item)
        {
            // Suche Item
            var orgin = Root.FindItem(Item);

            if (orgin != null)
            {
                Instance = orgin.Copy();
                Instance.Name = Name + "_" + Instance.Name;
                (Instance as Item).Parent = this;
            }
        }

        /// <summary>
        /// Liefert ein Schlüselbild zu der gegebenen Zeit
        /// </summary>
        /// <param name="time">Die Zeit</param>
        /// <returns>Das Schlüsselbid oder null</returns>
        public ItemVisualKeyFrame GetKeyFrame(ulong time)
        {
            ItemVisualKeyFrame prevKeyFrame = null;
            ItemVisualKeyFrame nextKeyFrame = null;

            foreach (var k in m_keyFrames)
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
                }
            }

            // Tweening
            if (prevKeyFrame != null && nextKeyFrame != null && prevKeyFrame.Tweening)
            {
                ulong from = prevKeyFrame.From + prevKeyFrame.Duration;
                ulong till = nextKeyFrame.From;

                Matrix3D mt = prevKeyFrame.Matrix.Invert * nextKeyFrame.Matrix;

                float t = (time - from) / (float)(till - from); //in %

                return new ItemVisualKeyFrameTweening()
                                {
                                    From = from,
                                    Duration = till - from,
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
        [Category("Daten"), Description("Die ID des mit der Instanz verbundenen Element.")]
        [XmlAttribute("item")]
        public string Item
        {
            get { return m_item; }
            set
            {
                if (m_item == null ? true : !m_item.Equals(value))
                {
                    m_item = value;
                    if (Instance == null)
                    {
                        AttachedInstance(Item);
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Die ID des mit der Instanz verbundenen Element
        /// </summary>
        [Browsable(false)]
        public IItem Instance { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [Category("Darstellung"), Description("Transformationsmatrix.")]
        [XmlElement("matrix")]
        public Matrix3D Matrix
        {
            get { return m_matrix; }
            set
            {
                if (!m_matrix.Equals(value))
                {
                    m_matrix = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Schlüselbilder
        /// </summary>
        [Category("Datan"), Description("Schlüsselbilder.")]
        [XmlIgnore]
        public ICollection<ItemVisualKeyFrame> KeyFrames
        {
            get { return m_keyFrames; }
        }

        /// <summary>
        /// Liefert die Größe des mit der Instanz verbundenen Objektes
        /// </summary>
        [Category("Darstellung"), Description("Objektgröße.")]
        [XmlIgnore]
        public Size ObjectSize
        {
            get
            {
                if (Instance != null && Instance is ItemVisualImage)
                {
                    return (Instance as ItemVisualImage).Image.Size;
                }

                return new Size();
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Größe des Objektes")]
        [XmlIgnore]
        public override Size Size
        {
            get
            {
                return ObjectSize;
            }
        }
    }
}
