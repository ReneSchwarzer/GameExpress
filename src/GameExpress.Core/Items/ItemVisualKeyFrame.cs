using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using GameExpress.Core.Structs;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("keyframe")]
    public class ItemVisualKeyFrame : ItemVisual
    {
        private ulong m_from;
        private ulong m_duration;
        private Matrix3D m_matrix;
        private bool m_tweening;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualKeyFrame()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualKeyFrame)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualKeyFrame(ItemContext context)
            :base(context, true)
        {
            Name = new ItemVisualKeyFrameContext().Name + "_" + GUID;
            Matrix = Matrix3D.Identity;
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
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualKeyFrame;
            copy.From = From;
            copy.Duration = Duration;
            copy.Matrix = Matrix;
            copy.Tweening = Tweening;

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt die Startzeit
        /// </summary>
        [Category("Zeiten"), Description("Startzeit in 1/40 Sekunden")]
        public ulong From
        {
            get { return m_from; }
            set
            {
                if (m_from != value)
                {
                    m_from = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Dauer
        /// </summary>
        [Category("Zeiten"), Description("Dauer in 1/40 Sekunden")]
        public ulong Duration
        {
            get { return m_duration; }
            set
            {
                if (m_duration != value)
                {
                    m_duration = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [Category("Darstellung"), Description("Transformationsmatrix")]
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
        /// Liefert oder setzt die Tweening-Eigenschaft
        /// </summary>
        [Category("Darstellung"), Description("Tweening")]
        public bool Tweening
        {
            get { return m_tweening; }
            set
            {
                if (!m_tweening.Equals(value))
                {
                    m_tweening = value;

                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Größe des Objektes")]
        public override Size Size
        {
            get
            {
                return new Size();
            }
        }
    }
}
