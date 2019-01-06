using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Drawing;

namespace GameExpress.Core.Items
{
    public class Tree<T> : ITree<T>, IXmlSerializable where T : Tree<T>
    {
        /// <summary>
        /// Liefert den Elternknoten
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public T Parent { get; set; }

        /// <summary>
        /// Liefere die Kinder
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        //[XmlArray(ElementName = "Items")]
        public ICollection<T> Children { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Tree()
        {
            Children = new List<T>();
        }

        /// <summary>
        /// Fügt ein Kind in dem Baum ein
        /// </summary>
        /// <param name="node">Das hinzuzufügende Kind</param>
        public void AddChild(T node)
        {
            Children.Add(node);
            node.Parent = this as T;
        }

        /// <summary>
        /// Entfernt ein Kind aus dem Baum
        /// </summary>
        /// <param name="node">Das zu entfernende Kind</param>
        public void RemoveChild(T node)
        {
            Children.Remove(node);
        }

        /// <summary>
        /// Zerstört den Knoten und entfernt diesen aus dem Elternknoten
        /// </summary>
        public void DestroyNode()
        {
            // Aus der Parent-Komponente entfernen
            if (Parent != null)
            {
                Parent.RemoveChild(this as T);
            }
        }

        /// <summary>
        /// Verschiebt den Knoten (inkl. Unterknoten)
        /// </summary>
        /// <param name="toNode">Der Knoten, welcher den aktuellen Knoten aufnimmt</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        public bool MoveNode(T toNode)
        {
            if (toNode == null || this == toNode || IsChild(toNode)) return false;

            if (Parent != null)
            {
                Parent.RemoveChild(this as T);
                toNode.AddChild(this as T);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Prüft, ob der Knoten ein Kind ist
        /// </summary>
        /// <param name="node">Der zu prüfende Knoten</param>
        /// <returns>true wenn Kind, sonst false</returns>
        public bool IsChild(T node)
        {
            if (node == null) return false;
            if (this == node) return true;

            if (Children.Contains(node))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlIgnore]
        public T Root
        {
            get
            {
                if (IsRoot) return this as T;

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
        [BrowsableAttribute(false)]
        [XmlIgnore]
        public bool IsRoot
        {
            get { return (Parent == null); }
        }

        /// <summary>
        /// Prüft, ob Knoten ein Blatt ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        [BrowsableAttribute(false)]
        [XmlIgnore]
        public bool IsLeaf
        {
            get { return (Children.Count == 0); }
        }

        /// <summary>
        /// Durchläuft den Baum in PreOrder
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        public IEnumerable<T> GetPreOrder()
        {
            var list = new List<T>();
            list.Add(this as T);

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
        public ICollection<T> GetPath()
        {
            var list = new List<T>();
            list.Add(this as T);

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
        /// Diese Methode ist reserviert und sollte nicht verwendet werden
        /// </summary>
        /// <returns>Ein XmlSchema zur Beschreibung der XML-Darstellung des Objekts, das von der WriteXml(XmlWriter)-Methode erstellt und von der ReadXml(XmlReader)-Methode verwendet wird.</returns>
        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generiert ein Objekt aus seiner XML-Darstellung
        /// </summary>
        /// <param name="reader">Der XmlReader-Stream, aus dem das Objekt deserialisiert wird.</param>
        public virtual void ReadXml(XmlReader reader)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Konvertiert ein Objekt in seine XML-Darstellung
        /// </summary>
        /// <param name="writer">Der XmlWriter-Stream, in den das Objekt serialisiert wird</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            WriteXml(writer, this);

            // Kinder
            foreach (var v in Children)
            {
                var type = v.GetType();
                var name = type.Name;

                foreach (var attribute in type.CustomAttributes.Where(x => x.AttributeType == typeof(XmlTypeAttribute)))
                {
                    var cname = attribute.ConstructorArguments.FirstOrDefault();
                    var nname = attribute.NamedArguments.FirstOrDefault();
                    name = !string.IsNullOrWhiteSpace(cname.Value?.ToString()) ?
                        cname.Value?.ToString() :
                        nname.TypedValue.Value?.ToString();
                    break;
                }

                writer.WriteStartElement(name);
                v.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Konvertiert ein Objekt in seine XML-Darstellung
        /// </summary>
        /// <param name="writer">Der XmlWriter-Stream, in den das Objekt serialisiert wird</param>
        /// <param name="obj">Das zu serialisierende Objekt</param>
        public virtual void WriteXml(XmlWriter writer, object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();

            // Attribute
            foreach (var p in properties)
            {
                foreach (var attribute in p.CustomAttributes.Where(x => x.AttributeType == typeof(XmlAttributeAttribute)))
                {
                    var cname = attribute.ConstructorArguments.FirstOrDefault();
                    var nname = attribute.NamedArguments.FirstOrDefault();
                    var name = !string.IsNullOrWhiteSpace(cname.Value?.ToString()) ?
                        cname.Value?.ToString() :
                        nname.TypedValue.Value?.ToString();
                    var value = p.GetValue(obj);

                    if (p.PropertyType == typeof(Color))
                    {
                        writer.WriteAttributeString(name, ((Color)value).ToArgb().ToString());
                    }
                    else
                    {
                        writer.WriteAttributeString(name, value?.ToString());
                    }
                }
            }

            // Elemente
            foreach (var p in properties)
            {
                foreach (var attribute in p.CustomAttributes.Where(x => x.AttributeType == typeof(XmlElementAttribute)))
                {
                    var cname = attribute.ConstructorArguments.FirstOrDefault();
                    var nname = attribute.NamedArguments.FirstOrDefault();
                    var name = !string.IsNullOrWhiteSpace(cname.Value?.ToString()) ?
                        cname.Value?.ToString() :
                        nname.TypedValue.Value?.ToString();
                    var value = p.GetValue(obj);

                    if (value == null)
                    {
                        break;
                    }

                    writer.WriteStartElement(name);

                    if (p.PropertyType == typeof(string))
                    {
                        writer.WriteString(value?.ToString());
                    }
                    else if (p.PropertyType == typeof(Point))
                    {
                        writer.WriteAttributeString("x", ((Point)value).X.ToString());
                        writer.WriteAttributeString("y", ((Point)value).Y.ToString());
                    }
                    else
                    {
                        WriteXml(writer, value);
                    }

                    writer.WriteEndElement();
                }
            }
        }
    }
}
