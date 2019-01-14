using GameExpress.Model.Structs;
using System;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("keyframe")]
    public class ItemKeyFrame : ItemKeyFrameBase
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
        /// Übergang
        /// </summary>
        private bool m_tweening;

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
        private short m_scaleX = 100;

        /// <summary>
        /// Die Skalierung der y-Achse
        /// </summary>
        private short m_scaleY = 100;

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
        public ItemKeyFrame()
        {
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemKeyFrame;
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
            copy.Tweening = Tweening;

            return copy as T;
        }

        /// <summary>
        /// Liefert oder setzt die relative Startzeit, bezogen auf den Vorgänger
        /// </summary>
        [XmlAttribute("from")]
        public ulong From
        {
            get { return m_from; }
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
            get { return m_duration; }
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
        /// Liefert oder setzt die Tweening-Eigenschaft
        /// </summary>
        [XmlAttribute("tweening")]
        public bool Tweening
        {
            get { return m_tweening; }
            set
            {
                if (!m_tweening.Equals(value))
                {
                    m_tweening = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [XmlIgnore]
        public override Matrix3D Matrix
        {
            get
            {
                var matrix = Matrix3D.Identity;

                matrix *= Matrix3D.RotationX(RotationX);
                matrix *= Matrix3D.RotationY(RotationY);
                matrix *= Matrix3D.RotationZ(RotationZ);
                matrix *= Matrix3D.Scaling(ScaleX / 100f, ScaleY / 100f);
                matrix *= Matrix3D.Translation(TranslationX, TranslationY);
                matrix *= Matrix3D.Shear(ShearX / 100f, ShearY / 100f);

                return matrix;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Liefert oder setzt die Rotation um die x-Achse
        /// </summary>
        [XmlAttribute("rotationx")]
        public short RotationX
        {
            get { return m_rotaionX; }
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
            get { return m_rotaionY; }
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
            get { return m_rotaionZ; }
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
            get { return m_translationX; }
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
            get { return m_translationY; }
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
        public short ScaleX
        {
            get { return m_scaleX; }
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
        public short ScaleY
        {
            get { return m_scaleY; }
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
            get { return m_shearX; }
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
            get { return m_shearY; }
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
        public override Size Size
        {
            get
            {
                return new Size();
            }
        }
    }
}