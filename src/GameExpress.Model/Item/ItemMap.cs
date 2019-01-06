using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    [XmlType("map")]
    public class ItemMap : ItemTreeNode
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
    }
}
