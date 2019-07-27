using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    [XmlType("map")]
    public class ItemMap : ItemTreeNode, IItemClickable
    {
        /// <summary>
        /// Liefert oder setzt die Vertices
        /// </summary>
        [XmlElement("vertex")]
        public ObservableCollection<ItemMapVertext> Vertices { get; set; } = new ObservableCollection<ItemMapVertext>();

        /// <summary>
        /// Liefert oder setzt die Maschen
        /// </summary>
        [XmlElement("mesh")]
        public ObservableCollection<ItemMapMesh> Mesh { get; set; } = new ObservableCollection<ItemMapMesh>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMap()
        {

        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            Vertices.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    foreach (var v in Vertices)
                    {
                        v.Map = null;
                        v.PropertyChanged -= OnMeshPropertyChanged;
                        v.Init();
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (ItemMapVertext v in e.NewItems)
                    {
                        v.Map = this;
                        v.PropertyChanged += OnVertextPropertyChanged;
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (ItemMapVertext v in e.OldItems)
                    {
                        v.Map = null;
                        v.PropertyChanged -= OnVertextPropertyChanged;
                    }
                }
            };

            Mesh.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    foreach (var v in Mesh)
                    {
                        v.Map = null;
                        v.PropertyChanged -= OnMeshPropertyChanged;
                        v.Init();
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (ItemMapMesh v in e.NewItems)
                    {
                        v.Map = this;
                        v.PropertyChanged += OnMeshPropertyChanged;
                    }
                }

                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (ItemMapMesh v in e.OldItems)
                    {
                        v.Map = null;
                        v.PropertyChanged -= OnMeshPropertyChanged;
                    }
                }
            };

            // Bestehende Objekte
            foreach (var v in Vertices)
            {
                v.Map = this;
                v.PropertyChanged += OnMeshPropertyChanged;
                v.Init();
            }

            foreach (var v in Mesh)
            {
                v.Map = this;
                v.PropertyChanged += OnMeshPropertyChanged;
                v.Init();
            }
        }

        /// <summary>
        /// Entfernen nicht mehr benötigter Ressourcen des Items
        /// </summary>
        public override void Dispose()
        {
            foreach (var v in Vertices)
            {
                v.Dispose();
            }

            foreach (var v in Mesh)
            {
                v.Dispose();
            }

            Vertices.Clear();
            Mesh.Clear();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            foreach (var v in Mesh)
            {
                v.Update(new UpdateContext(uc));
            }

            foreach (var v in Vertices)
            {
                v.Update(new UpdateContext(uc));
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            // Der Pfad ist ein nicht sichtbares Element, nur im Designer 
            // wird er visualisiert
            if (pc.Designer)
            {
                // Hintergrund
                Parent.Presentation(pc);

                foreach (var v in Mesh)
                {
                    v.Presentation(new PresentationContext(pc));
                }

                foreach (var v in Vertices)
                {
                    v.Presentation(new PresentationContext(pc));
                }
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemMap;

            return copy as T;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public virtual Item HitTest(HitTestContext hc, Vector point)
        {
            foreach (var v in Vertices)
            {
                var item = v.HitTest(hc, point);
                if (item != null)
                {
                    return item;
                }
            }

            foreach (var v in Mesh)
            {
                var item = v.HitTest(hc, point);
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich ein Mesh geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnVertextPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged("Vertices");
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich ein Mesh geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnMeshPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged("Mesh");
        }
    }
}
