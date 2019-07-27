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

namespace GameExpress.SelectionFrames
{
    public class SelectionFrameAnchor : ISelectionFrameAnchor
    {
        /// <summary>
        /// Liefert oder setzt die Position auf einem Orbit
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Liefert oder setzt den zugehörigen Frame
        /// </summary>
        protected ISelectionFrame Frame { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Koordinaten, die beim letzten Zeivhnen vorlagen
        /// </summary>
        public Vector CurrentPosition { get; private set; } = new Vector(0f);

        /// <summary>
        /// Liefert die Werkzeuge
        /// </summary>
        public ObservableCollection<ISelectionFrameHandle> Handles { get; } = new ObservableCollection<ISelectionFrameHandle>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Der zugehörige Frame</param>
        /// <param name="position">Die Position auf dem Orbit</param>
        public SelectionFrameAnchor(ISelectionFrame frame, Location position)
        {
            Frame = frame;
            Location = position;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="frame">Der zugehörige Frame</param>
        public SelectionFrameAnchor(ISelectionFrame frame)
            : this(frame, Location.None)
        {
        }

        /// <summary>
        /// Handle aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public virtual void Update(UpdateContext uc)
        {
            CurrentPosition = uc.Transform(new Vector(0f));

            // Updated die Handles
            foreach (var handle in Handles)
            {
                var newUC = new UpdateContext(uc)
                {
                    Matrix = Matrix3D.Translation(CurrentPosition) * GetMatrix(handle)
                };

                handle.Update(newUC);
            }
        }

        /// <summary>
        /// Zeichnet den Handle
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public virtual void Presentation(PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            // Zeichne die Handles
            foreach (var handle in Handles)
            {
                var newPC = new PresentationContext(pc)
                {
                    Matrix = Matrix3D.Translation(pc.Transform(new Vector(0f))) * GetMatrix(handle)
                };

                handle.Presentation(newPC);
            }
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Handle liegt und gibt das Handle zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Handle, welches gefunden wurde oder null</returns>
        public virtual ISelectionFrameHandle HitTest(HitTestContext hc, Vector point)
        {
            // Prüfe die Handles
            foreach (var handle in Handles)
            {
                var newHC = new HitTestContext(hc)
                {
                    Matrix = Matrix3D.Translation(hc.Transform(new Vector(0f))) * GetMatrix(handle)
                };

                if (handle.HitTest(newHC, point) != null)
                {
                    return handle;
                }
            }

            return null;
        }

        /// <summary>
        /// Berechnet die Matrix zu einem Handle
        /// </summary>
        /// <param name="handle">Der Handle</param>
        /// <returns>Die Matrix</returns>
        private Matrix3D GetMatrix(ISelectionFrameHandle handle)
        {
            var matrix = Matrix3D.Identity;
            var orbit = 0f;

            switch (handle.Orbit)
            {
                case Orbit.Low:
                    {
                        orbit = 30f;
                        break;
                    }
                case Orbit.Medium:
                    {
                        orbit = 60f;
                        break;
                    }
                case Orbit.Height:
                    {
                        orbit = 90f;
                        break;
                    }
                case Orbit.None:
                    return matrix;
            }

            switch (Location)
            {
                case Location.North:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.South).CurrentPosition;
                        // Verschiebung um Richtungsvektor 
                        if (a != b)
                        {
                            matrix *= Matrix3D.Translation((b - a).Unit * -orbit);
                        }

                        break;
                    }
                case Location.NorthEast:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.NorthWest).CurrentPosition;
                        // Ortsvektor D
                        var c = Frame.GetAnchor(Location.SouthEast).CurrentPosition;
                        // Richtungsvektor
                        var v = ((b - a).Unit + (c - a).Unit) * -orbit;
                        // Verschiebung um Richtungsvektor 
                        matrix *= Matrix3D.Translation(v);

                        break;
                    }
                case Location.East:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.West).CurrentPosition;
                        // Verschiebung um Richtungsvektor 
                        if (a != b)
                        {
                            matrix *= Matrix3D.Translation((b - a).Unit * -orbit);
                        }

                        break;
                    }
                case Location.SouthEast:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.SouthWest).CurrentPosition;
                        // Ortsvektor D
                        var c = Frame.GetAnchor(Location.NorthEast).CurrentPosition;
                        // Richtungsvektor
                        var v = ((b - a).Unit + (c - a).Unit) * -orbit;
                        // Verschiebung um Richtungsvektor 
                        matrix *= Matrix3D.Translation(v);

                        break;
                    }
                case Location.South:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.North).CurrentPosition;
                        // Verschiebung um Richtungsvektor 
                        if (a != b)
                        {
                            matrix *= Matrix3D.Translation((b - a).Unit * -orbit);
                        }

                        break;
                    }
                case Location.SouthWest:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.SouthEast).CurrentPosition;
                        // Ortsvektor D
                        var c = Frame.GetAnchor(Location.NorthWest).CurrentPosition;
                        // Richtungsvektor
                        var v = ((b - a).Unit + (c - a).Unit) * -orbit;
                        // Verschiebung um Richtungsvektor 
                        matrix *= Matrix3D.Translation(v);

                        break;
                    }
                case Location.West:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.East).CurrentPosition;
                        // Verschiebung um Richtungsvektor 
                        if (a != b)
                        {
                            matrix *= Matrix3D.Translation((b - a).Unit * -orbit);
                        }

                        break;
                    }
                case Location.NorthWest:
                    {
                        // Ortsvektor A
                        var a = CurrentPosition;
                        // Ortsvektor B
                        var b = Frame.GetAnchor(Location.NorthEast).CurrentPosition;
                        // Ortsvektor D
                        var c = Frame.GetAnchor(Location.SouthWest).CurrentPosition;
                        // Richtungsvektor
                        var v = ((b - a).Unit + (c - a).Unit) * -orbit;
                        // Verschiebung um Richtungsvektor 
                        matrix *= Matrix3D.Translation(v);

                        break;
                    }
            }

            return matrix;
        }

        /// <summary>
        /// Liefert ein Handle, weches sich auf dem angegeben Orbit befindet
        /// </summary>
        /// <param name="orbit">Der Orbit</param>
        /// <returns>Der Handle oder null</returns>
        public virtual ISelectionFrameHandle GetHandle(Orbit orbit)
        {
            foreach (var handle in Handles)
            {
                if (!handle.HightOrbit && handle.Orbit == orbit)
                {
                    return handle;
                }
                else if (handle.HightOrbit)
                {
                    switch (handle.Orbit)
                    {
                        case Orbit.Low:
                            if (orbit == Orbit.None)
                            {
                                return handle;
                            }
                            break;
                        case Orbit.Medium:
                            if (orbit == Orbit.Low)
                            {
                                return handle;
                            }
                            break;
                        case Orbit.Height:
                            if (orbit == Orbit.Medium)
                            {
                                return handle;
                            }
                            break;
                    }
                }
            }

            return null;
        }
    }
}
