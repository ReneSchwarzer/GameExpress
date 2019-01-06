using System;
using System.Collections.Generic;
using System.Text;

namespace GameExpress.Core.Items
{
    public interface ITree<T>
    {
        /// <summary>
        /// Liefert den Elternknoten
        /// </summary>
        T Parent { get; set; }

        /// <summary>
        /// Liefere die Kinder
        /// </summary>
        ICollection<T> Children { get; }

        /// <summary>
        /// F�gt ein Kind in dem Baum ein
        /// </summary>
        /// <param name="node">Das hinzuzuf�gende Kind</param>
        void AddChild(T pNode);

        /// <summary>
        /// Entfernt ein Kind aus dem Baum
        /// </summary>
        /// <param name="node">Das zu entfernende Kind</param>
        void RemoveChild(T pNode);

        /// <summary>
        /// Zerst�rt den Knoten und entfernt diesen aus dem Elternknoten
        /// </summary>
        void DestroyNode();

	    /// <summary>
        /// Verschiebt den Knoten (inkl. Unterknoten)
        /// </summary>
        /// <param name="toNode">Der Knoten, welcher den aktuellen Knoten aufnimmt</param>
        /// <returns>true wenn erfolgreich, sonst false</returns>
        bool MoveNode(T toNode);

        /// <summary>
        /// Liefert die Wurzel
        /// </summary>
        T Root { get; }

        /// <summary>
        /// Pr�ft, ob der Knoten ein Kind ist
        /// </summary>
        /// <param name="node">Der zu pr�fende Knoten</param>
        /// <returns>true wenn Kind, sonst false</returns>
        bool IsChild(T pNode);

        /// <summary>
        /// Pr�ft, ob der Knoten die Wurzel ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        bool IsRoot { get; }

        /// <summary>
        /// Pr�ft, ob Knoten ein Blatt ist
        /// </summary>
        /// <returns>true wenn Root, sonst false</returns>
        bool IsLeaf { get; }

        /// <summary>
        /// Durchl�uft den Baum in PreOrder
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        IEnumerable<T> GetPreOrder();

        /// <summary>
        /// Liefert den Pfad
        /// </summary>
        /// <returns>Der Pfad</returns>
        ICollection<T> GetPath();
    }
}
