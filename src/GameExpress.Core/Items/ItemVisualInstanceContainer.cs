using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using GameExpress.Core.Structs;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameExpress.Core.Items
{
    /// <summary>
    /// Aufnahme mehrerer Instanzen
    /// </summary>
    public abstract class ItemVisualInstanceContainer : ItemVisual
    {
        /// <summary>
        /// Liste der Instanzen
        /// </summary>
        private ObservableCollection<ItemVisualInstance> m_instanceItems = new ObservableCollection<ItemVisualInstance>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualInstanceContainer(ItemContext context, bool autoGUID)
            :base(context, autoGUID)
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            m_instanceItems.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (Item v in e.NewItems)
                    {
                        AddChild(v);
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
            base.Update(uc);

            foreach (var v in InstanceItems)
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

            if (InstanceItems == null) return;

            foreach (var v in InstanceItems.Reverse())
            {
                v.Presentation(new PresentationContext(pc) { Time = pc.Time });
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualInstanceContainer;
            foreach (var i in InstanceItems)
            {
                copy.InstanceItems.Add(i.Copy() as ItemVisualInstance);
            }

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt die Instanzen
        /// </summary>
        [Browsable(false)]
        public ICollection<ItemVisualInstance> InstanceItems
        {
            get { return m_instanceItems; }
        }
    }
}
