using GameExpress.Editor.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Controls
{
    public class TreeViewPathItem
    {
        /// <summary>
        /// Tritt ein, wenn das Item ausgewählt wird
        /// </summary>
        public event EventHandler SelectedItemChanged;

        public string ID { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public ContextMenuStrip ContextMenuStrip { get; set; }
        public Page Page { get; set; }
        public object Tag { get; set; }
        public Color ForeColor { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TreeViewPathItem()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der anzuzeigende Name</param>
        /// <param name="image">Das anzuzeigende Bild</param>
        public TreeViewPathItem(string name, Image image)
            : this(name, name, image)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Eindeutige ID</param>
        /// <param name="name">Der anzuzeigende Name</param>
        /// <param name="image">Das anzuzeigende Bild</param>
        public TreeViewPathItem(Guid id, string name, Image image)
            : this(id.ToString(), name, image)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Eindeutige ID</param>
        /// <param name="name">Der anzuzeigende Name</param>
        /// <param name="image">Das anzuzeigende Bild</param>
        public TreeViewPathItem(int id, string name, Image image)
            : this(id.ToString(), name, image)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Eindeutige ID</param>
        /// <param name="name">Der anzuzeigende Name</param>
        /// <param name="image">Das anzuzeigende Bild</param>
        public TreeViewPathItem(string id, string name, Image image)
        {
            ID = id;
            Name = name;
            Image = image;
            ForeColor = SystemColors.ControlText;
        }

        /// <summary>
        /// Löst das SelectedITemChanged-Event aus
        /// </summary>
        public void RaiseSelectedITemChanged()
        {
            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Umwandlung in String
        /// </summary>
        /// <returns>Das als String umgewandelte Objekt</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
