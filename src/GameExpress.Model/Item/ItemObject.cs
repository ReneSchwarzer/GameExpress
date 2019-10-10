using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.Model.Item
{
    [XmlType("object")]
    public class ItemObject : ItemGraphics, IItemStates, IItemState, IItemClipping
    {
        /// <summary>
        /// Liefert oder setzt die Objektzustände
        /// </summary>
        [XmlIgnore]
        public ObservableCollection<IItemState> States { get; set; } = new ObservableCollection<IItemState>();

        /// <summary>
        /// Der aktuelle Status
        /// </summary>
        private ItemAnimation m_currentState;

        /// <summary>
        /// Liefert oder setzt den aktuellen Status
        /// </summary>
        [XmlIgnore]
        public ItemAnimation CurrentState
        {
            get => m_currentState;

            set
            {
                if (value != m_currentState)
                {
                    m_currentState = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Size");
                }
            }
        }

        /// <summary>
        /// Abschneiden auf Größe des Objektes
        /// </summary>
        private bool m_clipping;

        /// <summary>
        /// Liefert oder setzt ob die Ausgabe auf Größe des Objektes abgeschnitten wird
        /// </summary>
        [XmlAttribute("clipping")]
        public bool Clipping
        {
            get => m_clipping;

            set
            {
                if (value != m_clipping)
                {
                    m_clipping = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemObject()
        {
            Children.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    States.Clear();
                }

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (Item add in e.NewItems)
                    {
                        if (add is ItemAnimation animation)
                        {
                            States.Add(animation);
                        }
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (Item remove in e.OldItems)
                    {
                        if (remove is ItemAnimation animation)
                        {
                            States.Remove(animation);
                        }
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
            if (CurrentState != null)
            {
                CurrentState.Update(uc);
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (!Enable)
            {
                return;
            }

            var transform = pc.Graphics.Transform;
           
            if (CurrentState != null)
            {
                if (Clipping)
                {
                    var origin = pc.Matrix.Transform(new Vector());
                    var size = pc.Matrix.Transform(CurrentState.Size);
                    var rect = new Rect(origin.X, origin.Y, size.X - origin.X, size.Y - origin.Y);

                    using (pc.Graphics.CreateLayer(1f, rect))
                    {
                        CurrentState.Presentation(pc);
                    }
                }
                else
                {
                    CurrentState.Presentation(pc);
                }
            }

            pc.Graphics.Transform = transform;
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public override Matrix3D GetMatrix()
        {
            return Matrix3D.Identity;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public override Item HitTest(HitTestContext hc, Vector point)
        {
            return null;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemObject>();
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemObject;
            copy.Clipping = Clipping;
            foreach(var state in States)
            {
                if (state is Item item)
                copy.States.Add(item as IItemState);
            }

            return copy as T;
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public override Vector Size => CurrentState != null ? CurrentState.Size : new Vector();
    }
}
