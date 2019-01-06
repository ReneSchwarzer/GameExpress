using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using GameExpress.Core.Structs;

namespace GameExpress.Core.Items
{
    [XmlType("map")]
    public class ItemMap : Item
    {
        /// <summary>
        /// Liefert oder setzt die Vertices
        /// </summary>
        [Browsable(false)]
        public IEnumerable<ItemMapVertext> Vertices
        {
            get { return FindItem<ItemMapVertext>(oneLevel: true); }
        }

        /// <summary>
        /// Liefert oder setzt die Maschen
        /// </summary>
        [Browsable(false)]
        public IEnumerable<ItemMapMesh> Mesh
        {
            get { return FindItem<ItemMapMesh>(oneLevel: true); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMap()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemMap)))
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemMap(ItemContext context)
            :base(context, true)
        {
            
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

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
            base.Presentation(pc);

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
        public override IItem Copy()
        {
            var copy = Context.ItemFactory() as ItemMap;

            foreach (var v in Vertices)
            {
                copy.AddChild(v.Copy() as ItemMapVertext);
            }
            
            return copy;
        }
    }
}
