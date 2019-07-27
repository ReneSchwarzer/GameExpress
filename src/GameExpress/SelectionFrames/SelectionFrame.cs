using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Vector = GameExpress.Model.Structs.Vector;

/// <summary>
/// ToDo: Mit XAML-Mitteln umsetzen und zu ManipulatorOverlay umbenennen
/// </summary>
namespace GameExpress.SelectionFrames
{
    /// <summary>
    /// Ein Auswahlrahmen
    /// </summary>
    public class SelectionFrame<T> : ISelectionFrame where T : Item
    {
        /// <summary>
        /// Liefert oder setzt den zugehörigen Verweis auf das Item 
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Rahmendicke
        /// </summary>
        protected int BorderThickness { get; set; } = 1;

        /// <summary>
        /// Liefert oder setzt die Anker
        /// </summary>
        protected IDictionary<Location, ISelectionFrameAnchor> Anchors { get; private set; } = new Dictionary<Location, ISelectionFrameAnchor>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit dem Auswahlrahmen verbundene Item</param>
        public SelectionFrame(T item)
        {
            Item = item;

            Anchors.Add(Location.North, new SelectionFrameAnchor(this, Location.North));
            Anchors.Add(Location.NorthEast, new SelectionFrameAnchor(this, Location.NorthEast));
            Anchors.Add(Location.East, new SelectionFrameAnchor(this, Location.East));
            Anchors.Add(Location.SouthEast, new SelectionFrameAnchor(this, Location.SouthEast));
            Anchors.Add(Location.South, new SelectionFrameAnchor(this, Location.South));
            Anchors.Add(Location.SouthWest, new SelectionFrameAnchor(this, Location.SouthWest));
            Anchors.Add(Location.West, new SelectionFrameAnchor(this, Location.West));
            Anchors.Add(Location.NorthWest, new SelectionFrameAnchor(this, Location.NorthWest));
            Anchors.Add(Location.HotSpot, new SelectionFrameAnchor(this, Location.HotSpot));
            Anchors.Add(Location.Center, new SelectionFrameAnchor(this, Location.Center));
        }

        /// <summary>
        /// Rahmen aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public virtual void Update(UpdateContext uc)
        {
            foreach (var anchors in Anchors.Values)
            {
                // Änderungen übernehmen
                var newUC = new UpdateContext(uc);
                newUC.Matrix *= GetMatrix(newUC, anchors, Item);

                anchors.Update(newUC);

                foreach(var handle in anchors.Handles)
                {
                    handle.Item = Item;
                }
            }

            var a = Anchors[Location.NorthEast].CurrentPosition;
            var b = Anchors[Location.SouthWest].CurrentPosition;

            var c = Anchors[Location.NorthWest].CurrentPosition;
            var d = Anchors[Location.SouthEast].CurrentPosition;

            var e = Anchors[Location.North].CurrentPosition;
            var f = Anchors[Location.South].CurrentPosition;

            var g = Anchors[Location.West].CurrentPosition;
            var h = Anchors[Location.East].CurrentPosition;

            var l = Math.Min(Math.Min((b - a).Length, (c - d).Length), Math.Min((f - e).Length, (h - g).Length));

            if (l == 0)
            {

            }
            else if (l < 100)
            {
                foreach (var anchors in Anchors.Values)
                {
                    foreach (var handles in anchors.Handles)
                    {
                        handles.HightOrbit = true;
                    }
                }
            }
            else
            {
                foreach (var anchors in Anchors.Values)
                {
                    foreach (var handles in anchors.Handles)
                    {
                        handles.HightOrbit = false;
                    }
                }
            }
        }

        /// <summary>
        /// Zeichnet dan Auswahlrahmen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void Presentation(PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            DrawFrame(pc);

            foreach (var anchors in Anchors.Values)
            {
                // Änderungen übernehmen
                var newPC = new PresentationContext(pc);

                newPC.Matrix *= GetMatrix(newPC, anchors, Item);

                anchors.Presentation(newPC);
            }

            DrawOverlay(pc);
            DrawDecuration(pc);
        }

        /// <summary>
        /// Zeichnet der Rahmen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawFrame(PresentationContext pc)
        {
            var accent = new UISettings().GetColorValue(UIColorType.Accent);

            var p0 = Anchors[Location.North].CurrentPosition;
            var p1 = Anchors[Location.NorthEast].CurrentPosition;
            var p2 = Anchors[Location.East].CurrentPosition;
            var p3 = Anchors[Location.SouthEast].CurrentPosition;
            var p4 = Anchors[Location.South].CurrentPosition;
            var p5 = Anchors[Location.SouthWest].CurrentPosition;
            var p6 = Anchors[Location.West].CurrentPosition;
            var p7 = Anchors[Location.NorthWest].CurrentPosition;

            // Rahmen zeichnen
            using (var geometry = CanvasGeometry.CreatePolygon(pc.Graphics, new Vector2[]
            {
                new Vector2((float)p0.X, (float)p0.Y),
                new Vector2((float)p1.X, (float)p1.Y),
                new Vector2((float)p2.X, (float)p2.Y),
                new Vector2((float)p3.X, (float)p3.Y),
                new Vector2((float)p4.X, (float)p4.Y),
                new Vector2((float)p5.X, (float)p5.Y),
                new Vector2((float)p6.X, (float)p6.Y),
                new Vector2((float)p7.X, (float)p7.Y)
            }))
            {
                pc.Graphics.DrawGeometry(geometry, accent, BorderThickness);
            }
        }


        /// <summary>
        /// Zeichnet das Overlay
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawOverlay(PresentationContext pc)
        {

        }

        /// <summary>
        /// Zeichnet die Dekuration
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void DrawDecuration(PresentationContext pc)
        {

        }

        /// <summary>
        /// Wechselt das Toolstet
        /// </summary>
        public virtual void SwitchToolset()
        {

        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        public virtual ISelectionFrameHandle HitTest(HitTestContext hc, Vector point)
        {
            foreach (var anchors in Anchors.Values)
            {
                var newHC = new HitTestContext(hc);

                // Änderungen übernehmen
                newHC.Matrix *= GetMatrix(newHC, anchors, Item);

                var handle = anchors.HitTest(newHC, point);
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
        protected virtual Matrix3D GetMatrix(IContext context, ISelectionFrameAnchor anchors, Item item)
        {
            var matrix = Matrix3D.Identity;

            if (item is IItemVisual visual)
            {
                switch (anchors.Location)
                {
                    case Location.North:
                        return Matrix3D.Translation
                        (
                            new Vector(visual.Size.Width / 2f, 0)
                        );
                    case Location.NorthWest:
                        return Matrix3D.Translation
                        (
                            new Vector(0, 0)
                        );
                    case Location.NorthEast:
                        return Matrix3D.Translation
                        (
                            new Vector(visual.Size.Width, 0)
                        );
                    case Location.South:
                        return Matrix3D.Translation
                        (
                            new Vector(visual.Size.Width / 2, visual.Size.Height)
                        );
                    case Location.SouthWest:
                        return Matrix3D.Translation
                        (
                            new Vector(0, visual.Size.Height)
                        );
                    case Location.SouthEast:
                        return Matrix3D.Translation
                        (
                            new Vector(visual.Size.Width, visual.Size.Height)
                        );
                    case Location.East:
                        return Matrix3D.Translation
                        (
                            new Vector(visual.Size.Width, visual.Size.Height / 2f)
                        );
                    case Location.West:
                        return Matrix3D.Translation
                        (
                            new Vector(0, visual.Size.Height / 2)
                        );
                    case Location.HotSpot:
                        {
                            if (visual is IItemHotSpot hotspot)
                            {
                                return Matrix3D.Translation(hotspot.Hotspot);
                            }
                            break;
                        }
                    case Location.Center:
                        {
                            return Matrix3D.Translation(visual.Size.Width / 2, visual.Size.Height / 2);
                        }
                }
            }

            // Änderungen übernehmen
            return matrix;
        }

        /// <summary>
        /// Liefert einen Anker
        /// </summary>
        /// <param name="location">Die Position des Ankers</param>
        /// <returns>Der Anker oder null</returns>
        public virtual ISelectionFrameAnchor GetAnchor(Location location)
        {
            if (Anchors.ContainsKey(location))
            {
                return Anchors[location];
            }

            return null;
        }
    }
}
