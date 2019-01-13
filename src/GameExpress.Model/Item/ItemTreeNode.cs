using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Baumknoten
    /// </summary>
    [XmlInclude(typeof(ItemVisual))]
    [XmlInclude(typeof(ItemSound))]
    [XmlInclude(typeof(ItemMap))]
    public abstract class ItemTreeNode : Item
    {
        /// <summary>
        /// Liefert die untergeordneten Knoten
        /// </summary>
        [XmlElement("item")]
        public ObservableCollection<ItemTreeNode> Children { get; private set; } = new ObservableCollection<ItemTreeNode>();

        /// <summary>
        /// Liefert den Elternknoten
        /// </summary>
        [XmlIgnore]
        public ItemTreeNode Parent { get; set; }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        [XmlIgnore]
        public ItemTreeNode Root
        {
            get
            {
                if (IsRoot) return this;

                var parent = Parent;
                while (!parent.IsRoot)
                {
                    parent = parent.Parent;
                }

                return parent;
            }
        }

        /// <summary>
        /// Prüft, ob der Knoten die Wurzel ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        [XmlIgnore]
        public bool IsRoot
        {
            get { return (Parent == null); }
        }

        /// <summary>
        /// Prüft, ob Knoten ein Blatt ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        [XmlIgnore]
        public bool IsLeaf
        {
            get { return (Children.Count == 0); }
        }

        /// <summary>
        /// Liefert den Pfad
        /// </summary>
        /// <returns>Der Pfad</returns>
        public string Path
        {
            get
            {
                var path = GetPath();

                return string.Join("/", path);
                    
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemTreeNode()
        {
            Children.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (ItemTreeNode v in e.NewItems)
                    {
                        v.Parent = this;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (ItemTreeNode v in e.OldItems)
                    {
                        v.Parent = null;
                    }
                }
            };
        }

        /// <summary>
        /// Durchläuft den Baum in PreOrder
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        public ICollection<ItemTreeNode> GetPreOrder()
        {
            var list = new List<ItemTreeNode>();
            list.Add(this);

            foreach (var v in Children)
            {
                list.AddRange(v.GetPreOrder());
            }

            return list;
        }

        /// <summary>
        /// Liefert den Pfad
        /// </summary>
        /// <returns>Der Pfad</returns>
        public ICollection<ItemTreeNode> GetPath()
        {
            var list = new List<ItemTreeNode>();
            list.Add(this);

            var parent = Parent;
            while (parent != null)
            {
                list.Add(parent);

                parent = parent.Parent;
            }

            list.Reverse();

            return list;
        }

        /// <summary>
        /// Sucht ein Item anahnd des Namens
        /// </summary>
        /// <param name="name">Der Name des gesuchten Items</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        public ItemTreeNode FindItem(string name, bool oneLevel = false)
        {
            var list = new List<ItemTreeNode>
            (
                oneLevel ? Children : GetPreOrder()
            );

            return list.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        /// <summary>
        /// Sucht ein Item anahnd verschiedener Parameter
        /// </summary>
        /// <param name="type">Der Typ des gesuchten Items</param>
        /// <param name="name">Der Name des gesuchten Items</param>
        /// <param name="oneLevel">Die Suche beschränkt sich auf die nächste Ebene</param>
        /// <returns>Das Item oder null</returns>
        public IEnumerable<T> FindItem<T>(string name = null, bool oneLevel = false) where T : Item
        {
            if (string.IsNullOrWhiteSpace(name) && oneLevel)
            {
                return Children.Where(x => x.GetType().Equals(typeof(T))).Select(x => x as T);
            }
            else if (!string.IsNullOrWhiteSpace(name) && oneLevel)
            {
                return Children.Where
                (
                    x => x.GetType().Equals(typeof(T)) &&
                    (x as Item).Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                ).Select
                (
                    x => x as T
                );
            }
            else if (string.IsNullOrWhiteSpace(name) && !oneLevel)
            {
                return GetPreOrder().Where(x => x.GetType().Equals(typeof(T))).Select(x => x as T);
            }
            else if (!string.IsNullOrWhiteSpace(name) && !oneLevel)
            {
                return GetPreOrder().Where
                (
                    x => x.GetType().Equals(typeof(T)) &&
                    (x as Item).Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                ).Select
                (
                    x => x as T
                );
            }

            return new List<T>();
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Der Baumknoten in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
