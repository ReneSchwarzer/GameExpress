using GameExpress.Context;
using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.SelectionFrames
{
    /// <summary>
    /// Ein Auswahlrahmen
    /// </summary>
    public class SelectionFrameMap : SelectionFrame<ItemMap>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit dem Auswahlrahmen verbundene Item</param>
        public SelectionFrameMap(ItemMap item)
            : base(item)
        {
        }

        /// <summary>
        /// Rahmen aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            if (!uc.Designer)
            {
                return;
            }

            if (Item is ItemMap map)
            {
                // Eventhandler merken
                var dict = new Dictionary<ISelectionFrameAnchor, EventHandler<EventArgsRetrievePosition>>();
                
                // Alle verwendeten Handles
                var handles = GetAnchors(Location.Owner)?.Select(x => x.GetHandle(Orbit.None)).ToList();
                var items = handles?.Select(y => y.Item).ToList();
               
                // Lösche nicht mehr benötigte Handles
                if (handles != null)
                {
                    foreach (var v in handles?.Where(x => !map.Vertices.Contains(x.Item)))
                    {
                        v.Anchor.RetrievePosition -= dict[v.Anchor];
                        RemoveAnchor(Location.Owner, v.Anchor);
                    }
                }

                // Neue Handles hinzufügen 
                foreach (var v in map.Vertices.Where(x => items == null || !items.Contains(x)))
                {
                    var anchor = new SelectionFrameAnchor(this, Location.Owner);
                    var handle = new SelectionFrameHandleSizeMove(this, anchor)
                    {
                        Item = v,
                        Orbit = Orbit.None
                    };
                    anchor.Handles.Add(handle);

                    dict[anchor] = (s, e) =>
                    {
                        e.Position = v.Vector - map.Location;
                    };

                    anchor.RetrievePosition += dict[anchor];

                    AddAnchor(Location.Owner, anchor);
                }

                // Frame updaten 
                base.Update(uc);
            }
        }

        /// <summary>
        /// Zeichnet den Auswahlrahmen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            if (Item is ItemMap)
            {
                // Frame zeichnen 
                base.Presentation(pc);
            }
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        public override ISelectionFrameHandle HitTest(HitTestContext hc, Vector point)
        {
            if (Item is ItemMap)
            {
                var handle = base.HitTest(hc, point);
                if (handle != null)
                {
                    return handle;
                }
            }

            return null;
        }

        /// <summary>
        /// Erstellt die Matrix zu einem Handle
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="anchors">Der Handle, für den der PC berechnet werden soll</param>
        /// <returns>Die aktuelle Matrix der Instanz</returns>
        /// <param name="item">Das Item</param>
        protected override Matrix3D GetMatrix(IContext context, ISelectionFrameAnchor anchors, Item item)
        {
            if (item is ItemMap map)
            {
                return Matrix3D.Translation(map.Location) * base.GetMatrix(context, anchors, map);
            }

            return Matrix3D.Identity;
        }
    }
}
