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
        protected IDictionary<Location, List<ISelectionFrameAnchor>> Anchors { get; private set; } = new Dictionary<Location, List<ISelectionFrameAnchor>>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit dem Auswahlrahmen verbundene Item</param>
        public SelectionFrame(T item)
        {
            Item = item;

            AddAnchor(Location.North, new SelectionFrameAnchor(this, Location.North));
            AddAnchor(Location.NorthEast, new SelectionFrameAnchor(this, Location.NorthEast));
            AddAnchor(Location.East, new SelectionFrameAnchor(this, Location.East));
            AddAnchor(Location.SouthEast, new SelectionFrameAnchor(this, Location.SouthEast));
            AddAnchor(Location.South, new SelectionFrameAnchor(this, Location.South));
            AddAnchor(Location.SouthWest, new SelectionFrameAnchor(this, Location.SouthWest));
            AddAnchor(Location.West, new SelectionFrameAnchor(this, Location.West));
            AddAnchor(Location.NorthWest, new SelectionFrameAnchor(this, Location.NorthWest));
            AddAnchor(Location.HotSpot, new SelectionFrameAnchor(this, Location.HotSpot));
            AddAnchor(Location.Center, new SelectionFrameAnchor(this, Location.Center));
        }

        /// <summary>
        /// Rahmen aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public virtual void Update(UpdateContext uc)
        {
            foreach (var anchors in Anchors.Values.SelectMany(x => x))
            {
                // Änderungen übernehmen
                var newUC = new UpdateContext(uc);
                newUC.Matrix *= GetMatrix(newUC, anchors, Item);

                anchors.Update(newUC);
            }

            var a = GetAnchor(Location.NorthEast).CurrentPosition;
            var b = GetAnchor(Location.SouthWest).CurrentPosition;

            var c = GetAnchor(Location.NorthWest).CurrentPosition;
            var d = GetAnchor(Location.SouthEast).CurrentPosition;

            var e = GetAnchor(Location.North).CurrentPosition;
            var f = GetAnchor(Location.South).CurrentPosition;

            var g = GetAnchor(Location.West).CurrentPosition;
            var h = GetAnchor(Location.East).CurrentPosition;

            var l = Math.Min
                    (
                        Math.Min((b - a).Length, (c - d).Length),
                        Math.Min((f - e).Length, (h - g).Length)
                    );

            if (l == 0)
            {

            }
            else if (l < 100)
            {
                foreach (var anchors in Anchors.Values.SelectMany(x => x))
                {
                    foreach (var handles in anchors.Handles)
                    {
                        if (anchors.Location != Location.Owner &&
                            anchors.Location != Location.Center &&
                            anchors.Location != Location.HotSpot)
                        {
                            handles.HightOrbit = true;
                        }
                    }
                }
            }
            else
            {
                foreach (var anchors in Anchors.Values.SelectMany(x => x))
                {
                    foreach (var handles in anchors.Handles)
                    {
                        if (anchors.Location != Location.Owner &&
                            anchors.Location != Location.Center &&
                            anchors.Location != Location.HotSpot)
                        {
                            handles.HightOrbit = false;
                        }
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

            foreach (var anchors in Anchors.Values.SelectMany(x => x))
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

            var p0 = GetAnchor(Location.North).CurrentPosition;
            var p1 = GetAnchor(Location.NorthEast).CurrentPosition;
            var p2 = GetAnchor(Location.East).CurrentPosition;
            var p3 = GetAnchor(Location.SouthEast).CurrentPosition;
            var p4 = GetAnchor(Location.South).CurrentPosition;
            var p5 = GetAnchor(Location.SouthWest).CurrentPosition;
            var p6 = GetAnchor(Location.West).CurrentPosition;
            var p7 = GetAnchor(Location.NorthWest).CurrentPosition;

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
            foreach (var anchors in Anchors.Values.SelectMany(x => x))
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

            if (item is IItemSizing sizing)
            {
                switch (anchors.Location)
                {
                    case Location.North:
                        return Matrix3D.Translation
                        (
                            new Vector(sizing.Size.X / 2f, 0)
                        );
                    case Location.NorthWest:
                        return Matrix3D.Translation
                        (
                            new Vector(0, 0)
                        );
                    case Location.NorthEast:
                        return Matrix3D.Translation
                        (
                            new Vector(sizing.Size.X, 0)
                        );
                    case Location.South:
                        return Matrix3D.Translation
                        (
                            new Vector(sizing.Size.X / 2, sizing.Size.Y)
                        );
                    case Location.SouthWest:
                        return Matrix3D.Translation
                        (
                            new Vector(0, sizing.Size.Y)
                        );
                    case Location.SouthEast:
                        return Matrix3D.Translation
                        (
                            new Vector(sizing.Size.X, sizing.Size.Y)
                        );
                    case Location.East:
                        return Matrix3D.Translation
                        (
                            new Vector(sizing.Size.X, sizing.Size.Y / 2f)
                        );
                    case Location.West:
                        return Matrix3D.Translation
                        (
                            new Vector(0, sizing.Size.Y / 2)
                        );
                    case Location.HotSpot:
                        {
                            if (item is IItemHotSpot hotspot)
                            {
                                return Matrix3D.Translation(hotspot.Hotspot);
                            }
                            break;
                        }
                    case Location.Center:
                        {
                            return Matrix3D.Translation(sizing.Size.X / 2, sizing.Size.Y / 2);
                        }
                }
            }

            // Änderungen übernehmen
            return matrix;
        }

        /// <summary>
        /// Liefert einen Anker einer Position
        /// </summary>
        /// <param name="location">Die Position des Ankers</param>
        /// <returns>Der Anker oder null</returns>
        public virtual ISelectionFrameAnchor GetAnchor(Location location)
        {
            if (Anchors.ContainsKey(location))
            {
                return Anchors[location].FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// Liefert einen alle Anker einer Position
        /// </summary>
        /// <param name="location">Die Position des Ankers</param>
        /// <returns>Der Anker oder null</returns>
        public virtual List<ISelectionFrameAnchor> GetAnchors(Location location)
        {
            if (Anchors.ContainsKey(location))
            {
                return Anchors[location];
            }

            return null;
        }

        /// <summary>
        /// Fügt einen neuen Anker einer Position hinzu
        /// </summary>
        /// <param name="location">Die Position des Ankers<</param>
        /// <param name="anchor">Der Anker, der hinzugefügt werden soll</param>
        public void AddAnchor(Location location, ISelectionFrameAnchor anchor)
        {
            if (!Anchors.ContainsKey(location))
            {
                Anchors[location] = new List<ISelectionFrameAnchor>();
            }

            Anchors[location].Add(anchor);
        }

        /// <summary>
        /// Entfert einen Anker einer Position
        /// </summary>
        /// <param name="location">Die Position des Ankers<</param>
        /// <param name="anchor">Der Anker, der entfernt werden soll</param>
        public void RemoveAnchor(Location location, ISelectionFrameAnchor anchor)
        {
            if (Anchors.ContainsKey(location))
            {
                Anchors[location].Remove(anchor);
            }
        }

        /// <summary>
        /// Entfert alle Anker einer Position
        /// </summary>
        /// <param name="location">Die Position des Ankers<</param>
        public void RemoveAnchors(Location location)
        {
            if (Anchors.ContainsKey(location))
            {
                Anchors[location].Clear();
            }
        }

        /// <summary>
        /// Fügt einen neuen Handle einer Position hinzu
        /// </summary>
        /// <param name="location">Die Position des Handle<</param>
        /// <param name="handle">Der Handle, der hinzugefügt werden soll</param>
        public void AddHandle(Location location, ISelectionFrameHandle handle)
        {
            var anchor = GetAnchor(location);
            anchor?.Handles.Add(handle);
        }

        /// <summary>
        /// Entfert alle Handles einer Position
        /// </summary>
        /// <param name="location">Die Position des Ankers<</param>
        public void RemoveHandles(Location location)
        {
            var anchor = GetAnchor(location);
            anchor?.Handles.Clear();
        }
    }
}
