using GameExpress.Editor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Controls
{
    public class TreeViewPathCollection : List<TreeViewPathItem>
    {
        /// <summary>
        /// Liefert die Seite
        /// </summary>
        public Page Page { get { return this.Last().Page; } }

        /// <summary>
        /// Liefert das verbundene Objekt
        /// </summary>
        public object Item { get { return this.Last().Tag; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathCollection()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathCollection(TreeViewPathCollection list)
            : base(list)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathCollection(TreeViewPathItem item)
        {
            Add(item);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathCollection(TreeViewPathItem[] items)
            : base(items)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathCollection(IEnumerable<TreeViewPathItem> items)
            : base(items)
        {
        }

        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <returns>Das als String umgewandelte Objekt</returns>
        public override string ToString()
        {
            return string.Join("/", this);
        }
    }
}
