using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Controls
{
    public partial class TreeViewPath : TreeView
    {
        private Dictionary<TreeNode, TreeViewPathCollection> Items { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPath()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            Items = new Dictionary<TreeNode, TreeViewPathCollection>();
            ImageList = new ImageList() { ColorDepth = ColorDepth.Depth24Bit, ImageSize = new Size(16, 16) };
        }

        /// <summary>
        /// Fügt ein Pfad hinzu
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void InsertNode(TreeViewPathCollection path)
        {
            UpdateNode(path);

        }

        /// <summary>
        /// Aktualisiert ein Pfad
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void UpdateNode(TreeViewPathCollection path)
        {
            var parent = Nodes;
            var buf = new TreeViewPathCollection();

            foreach (var item in path)
            {
                buf.Add(item);
                var node = FindNode(buf);
                var index = -1;

                // Image festlegen
                if (item.Image != null && !ImageList.Images.ContainsKey(item.Image.GetHashCode().ToString()))
                {
                    ImageList.Images.Add(item.Image.GetHashCode().ToString(), item.Image);
                }

                if (item.Image != null)
                {
                    index = ImageList.Images.IndexOfKey(item.Image.GetHashCode().ToString());
                }

                if (node == null)
                {
                    // Knoten anlegen
                    node = new TreeNode(item.Name);
                    parent.Add(node);

                    InsertItem(node, new TreeViewPathCollection(buf));
                }

                node.Text = item.Name;
                node.ImageIndex = index;
                node.SelectedImageIndex = index;
                node.ContextMenuStrip = item.ContextMenuStrip;
                node.ForeColor = item.ForeColor;

                parent = node.Nodes;
            }
        }

        /// <summary>
        /// Löscht ein Pfad
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void RemoveNode(TreeViewPathCollection path)
        {
            RemoveNode(Nodes, path);
        }

        /// <summary>
        /// Löscht alle Pfade
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void RemoveAllNode()
        {
            Items.Clear();
            Nodes.Clear();
        }

        /// <summary>
        /// Löscht ein Pfad
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void RemoveNode(TreeNodeCollection parents, TreeViewPathCollection path)
        {
            var node = FindNode(path);

            if (node != null)
            {
                node.Tag = null;
                node.ImageIndex = -1;
                node.SelectedImageIndex = -1;

                if (node.Parent != null && node.Nodes.Count == 0)
                {
                    node.Parent.Nodes.Remove(node);
                }
                else if (node.Parent != null)
                {
                    node.Parent.Nodes.Remove(node);
                }
                else if (node.Parent == null)
                {
                    Nodes.Remove(node);
                }

                RemoveItem(node);
            }
        }

        /// <summary>
        /// Sucht ein Knoten
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public TreeNode FindNode(TreeViewPathCollection path)
        {
            foreach (var item in Items)
            {
                var node = item.Key;

                if (item.Value.Count != path.Count) continue;

                var itemIDs = string.Join("/", from x in item.Value select x.ID);
                var pathIDs = string.Join("/", from x in path select x.ID);

                if (itemIDs.Equals(pathIDs, StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Ermittelt die Informationen aus dem Tag
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TreeViewPathCollection GetItem(TreeNode node)
        {
            if (node != null && Items.ContainsKey(node))
            {
                return Items[node];
            }

            return null;
        }

        /// <summary>
        /// Fügt ein Item hinzu
        /// </summary>
        /// <param name="node">Der Baumknoten</param>
        /// <param name="item">Das zugehörige Item</param>
        protected void InsertItem(TreeNode node, TreeViewPathCollection item)
        {
            if (!Items.ContainsKey(node))
            {
                Items.Add(node, null);
            }

            Items[node] = item;
        }

        /// <summary>
        /// Entfernt ein Item
        /// </summary>
        /// <param name="node">Der Baumknoten</param>
        /// <param name="item">Das zugehörige Item</param>
        protected void RemoveItem(TreeNode node)
        {
            if (Items.ContainsKey(node))
            {
                Items.Remove(node);
            }
        }

        /// <summary>
        /// Liefert den Pfad zu dem aktiven Baumknoten
        /// </summary>
        public TreeViewPathCollection SelectedPath
        {
            get { return GetPath(SelectedNode); }
            set
            {
                foreach (var item in Items)
                {
                    var node = FindNode(value);
                    if (SelectedNode != node)
                    {
                        SelectedNode = node;
                    }
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Knoten ausgewählt wurd
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            var item = GetItem(e.Node);

            if (item != null)
            {
                item.Last().RaiseSelectedITemChanged();
            }
        }


        /// <summary>
        /// Liefert den Pfad zu einem Baumknoten
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        protected TreeViewPathCollection GetPath(TreeNode node)
        {
            return GetItem(node);
        }

        /// <summary>
        /// Wandelt den Baum als Liste um 
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        public List<TreeViewPathCollection> PreOrder()
        {
            var nodeList = new List<TreeViewPathCollection>();
            foreach (var n in Nodes)
            {
                nodeList.Add(GetItem(n as TreeNode));
                nodeList.AddRange(PreOrder(n as TreeNode));
            }

            return nodeList;
        }

        /// <summary>
        /// Wandelt den Baum als Liste um 
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        protected List<TreeViewPathCollection> PreOrder(TreeNode parent)
        {
            var nodeList = new List<TreeViewPathCollection>();

            if (parent == null) return nodeList;

            // Knoten vorhanden? 
            foreach (TreeNode n in parent.Nodes)
            {
                var item = GetItem(n);

                nodeList.Add(item);

                var buf = PreOrder(n);
                nodeList.AddRange(buf);
            }

            return nodeList;
        }

        /// <summary>
        /// Erweitert den ausgewählten Pfad
        /// </summary>
        public void ExpandSelectedPath()
        {
            if (SelectedPath == null) return;

            var node = FindNode(SelectedPath);

            if (node == null) return;

            node.ExpandAll();
        }
    }
}
