using GameExpress.Model.Structs;
using System;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("act")]
    public class ItemKeyFrameAct : ItemKeyFrame, IItemVisual, IItemSizing, IItemClickable, IItemTranslation, IItemScale
    {
        /// <summary>
        /// Relativer Beginn, ausgehend vom vorherigen Keyframe
        /// </summary>
        private ulong m_from;

        /// <summary>
        /// Dauer
        /// </summary>
        private ulong m_duration;

        /// <summary>
        /// Die Rotation um die x-Achse
        /// </summary>
        private short m_rotaionX = 0;

        /// <summary>
        /// Die Rotation um die x-Achse
        /// </summary>
        private short m_rotaionY = 0;

        /// <summary>
        /// Die Rotation um die x-Achse
        /// </summary>
        private short m_rotaionZ = 0;

        /// <summary>
        /// Die Verschiebung der x-Achse entlang
        /// </summary>
        private short m_translationX = 0;

        /// <summary>
        /// Die Verschiebung der y-Achse entlang
        /// </summary>
        private short m_translationY = 0;

        /// <summary>
        /// Die Skalierung der x-Achse
        /// </summary>
        private double m_scaleX = 100;

        /// <summary>
        /// Die Skalierung der y-Achse
        /// </summary>
        private double m_scaleY = 100;

        /// <summary>
        /// Die Scherung der x-Achse
        /// </summary>
        private short m_shearX = 0;

        /// <summary>
        /// Die Scherung der y-Achse
        /// </summary>
        private short m_shearY = 0;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemKeyFrameAct()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            var newUC = new UpdateContext(uc)
            {
                Matrix = uc.Matrix * GetMatrix()
            };

            base.Update(newUC);

            Story.Instance?.Update(newUC);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            var newPC = new PresentationContext(pc)
            {
                Matrix = pc.Matrix * GetMatrix()
            };

            base.Presentation(newPC);

            Story.Instance?.Presentation(newPC);
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public virtual Matrix3D GetMatrix()
        {
            var matrix = Matrix3D.Identity;

            matrix *= Matrix3D.RotationX(RotationX);
            matrix *= Matrix3D.RotationY(RotationY);
            matrix *= Matrix3D.RotationZ(RotationZ);
            matrix *= Matrix3D.Scaling((ScaleX == 0 ? 0.1f : ScaleX) / 100f, (ScaleY == 0 ? 0.1f : ScaleY) / 100f);
            matrix *= Matrix3D.Translation(TranslationX, TranslationY);
            matrix *= Matrix3D.Shear(ShearX / 100f, ShearY / 100f);

            if (Story.Instance is IItemHotSpot item)
            {
                matrix *= Matrix3D.Translation(item.Hotspot.X * -1, item.Hotspot.Y * -1);
            }

            return matrix;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public virtual Item HitTest(HitTestContext hc, Vector point)
        {
            var newHC = new HitTestContext(hc)
            {
                Matrix = hc.Matrix * GetMatrix()
            };

            if (Story.Instance?.Instance is IItemClickable instance)
            {
                if (instance.HitTest(newHC, point) != null)
                {
                    return Story;
                };
            }

            return null;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemKeyFrameAct>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemKeyFrameAct;
            copy.From = From;
            copy.Duration = Duration;
            copy.RotationX = RotationX;
            copy.RotationY = RotationY;
            copy.RotationZ = RotationZ;
            copy.TranslationX = TranslationX;
            copy.TranslationY = TranslationY;
            copy.ScaleX = ScaleX;
            copy.ScaleY = ScaleY;
            copy.ShearX = ShearX;
            copy.ShearY = ShearY;

            return copy as T;
        }

        /// <summary>
        /// Liefert oder setzt die relative Startzeit, bezogen auf den Vorgänger
        /// </summary>
        [XmlAttribute("from")]
        public ulong From
        {
            get => m_from;
            set
            {
                if (m_from != value)
                {
                    m_from = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Dauer in 1/40 Sekunden
        /// </summary>
        [XmlAttribute("duration")]
        public ulong Duration
        {
            get => m_duration;
            set
            {
                if (m_duration != value)
                {
                    m_duration = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Rotation um die x-Achse
        /// </summary>
        [XmlAttribute("rotationx")]
        public short RotationX
        {
            get => m_rotaionX;
            set
            {
                if (!m_rotaionX.Equals(value))
                {
                    m_rotaionX = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Rotation um die y-Achse
        /// </summary>
        [XmlAttribute("rotationy")]
        public short RotationY
        {
            get => m_rotaionY;
            set
            {
                if (!m_rotaionY.Equals(value))
                {
                    m_rotaionY = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Rotation um die z-Achse
        /// </summary>
        [XmlAttribute("rotationz")]
        public short RotationZ
        {
            get => m_rotaionZ;
            set
            {
                if (!m_rotaionZ.Equals(value))
                {
                    m_rotaionZ = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Verschiebung der x-Achse entlang
        /// </summary>
        [XmlAttribute("translationx")]
        public short TranslationX
        {
            get => m_translationX;
            set
            {
                if (!m_translationX.Equals(value))
                {
                    m_translationX = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Verschiebung der y-Achse entlang
        /// </summary>
        [XmlAttribute("translationy")]
        public short TranslationY
        {
            get => m_translationY;
            set
            {
                if (!m_translationY.Equals(value))
                {
                    m_translationY = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Skalierung der x-Achse
        /// </summary>
        [XmlAttribute("sclaex")]
        public double ScaleX
        {
            get => m_scaleX;
            set
            {
                if (!m_scaleX.Equals(value))
                {
                    m_scaleX = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Skalierung der y-Achse
        /// </summary>
        [XmlAttribute("scaley")]
        public double ScaleY
        {
            get => m_scaleY;
            set
            {
                if (!m_scaleY.Equals(value))
                {
                    m_scaleY = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Scherung der x-Achse
        /// </summary>
        [XmlAttribute("shearx")]
        public short ShearX
        {
            get => m_shearX;
            set
            {
                if (!m_shearX.Equals(value))
                {
                    m_shearX = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Scherung der y-Achse
        /// </summary>
        [XmlAttribute("sheary")]
        public short ShearY
        {
            get => m_shearY;
            set
            {
                if (!m_shearY.Equals(value))
                {
                    m_shearY = value;

                    RaisePropertyChanged();
                }
            }
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