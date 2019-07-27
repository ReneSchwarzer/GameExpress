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
    public class SelectionFrameStory : SelectionFrame<ItemStory>
    {
        /// <summary>
        /// Die Werkzeugzusammenstellung
        /// </summary>
        private enum Toolset { Sizing, Rotation, Shearing }

        /// <summary>
        /// Liefert oder setzt das aktuelle Frame
        /// </summary>
        private ItemKeyFrame Frame { get; set; }

        /// <summary>
        /// Liefert oder setzt die aktuelle Werkzegzusammenstellung
        /// </summary>
        private Toolset CurrentToolset { get; set; } = Toolset.Sizing;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit dem Auswahlrahmen verbundene Item</param>
        public SelectionFrameStory(ItemStory item)
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

            if (Item is ItemStory story && story.GetKeyFrame((ulong)story.LocalTime(uc.Time)) is ItemKeyFrame frame)
            {
                Frame = frame;

                var newUC = new UpdateContext(uc)
                {
                    Time = story.LocalTime(uc.Time)
                };

                if (frame is IItemVisual visual)
                {
                    newUC.Matrix *= visual.GetMatrix();
                }

                // Frame updaten 
                base.Update(newUC);

                foreach (var handle in Anchors.Values.SelectMany(x => x.Handles))
                {
                    handle.Item = Frame;
                }
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

            if (Item is ItemStory story && Frame != null)
            {
                var newPC = new PresentationContext(pc)
                {
                    Time = story.LocalTime(pc.Time)
                };

                if (Frame is IItemVisual visual)
                {
                    newPC.Matrix *= visual.GetMatrix();
                }

                // Frame zeichnen 
                base.Presentation(newPC);
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
            if (Item is ItemStory story && Frame != null)
            {
                var newHC = new HitTestContext(hc)
                {
                    Time = story.LocalTime(hc.Time)
                };

                if (Frame is IItemVisual visual)
                {
                    newHC.Matrix *= visual.GetMatrix();
                }

                // Frame zeichnen 
                var handle = base.HitTest(newHC, point);
                if (handle != null)
                {
                    return handle;
                }
            }

            return null;
        }

        /// <summary>
        /// Wechselt das Toolstet
        /// </summary>
        public override void SwitchToolset()
        {
            Anchors[Location.North].Handles.Clear();
            Anchors[Location.NorthEast].Handles.Clear();
            Anchors[Location.East].Handles.Clear();
            Anchors[Location.SouthEast].Handles.Clear();
            Anchors[Location.South].Handles.Clear();
            Anchors[Location.SouthWest].Handles.Clear();
            Anchors[Location.West].Handles.Clear();
            Anchors[Location.NorthWest].Handles.Clear();

            switch (CurrentToolset)
            {
                case Toolset.Sizing:
                    // Wechsel zu Rotation
                    CurrentToolset = Toolset.Rotation;

                    Anchors[Location.North].Handles.Add(new SelectionFrameHandleSizeN(this, Anchors[Location.North]));
                    Anchors[Location.NorthEast].Handles.Add(new SelectionFrameHandleSizeNE(this, Anchors[Location.NorthEast]));
                    Anchors[Location.East].Handles.Add(new SelectionFrameHandleSizeE(this, Anchors[Location.East]));
                    Anchors[Location.SouthEast].Handles.Add(new SelectionFrameHandleSizeSE(this, Anchors[Location.SouthEast]));
                    Anchors[Location.South].Handles.Add(new SelectionFrameHandleSizeS(this, Anchors[Location.South]));
                    Anchors[Location.SouthWest].Handles.Add(new SelectionFrameHandleSizeSW(this, Anchors[Location.SouthWest]));
                    Anchors[Location.West].Handles.Add(new SelectionFrameHandleSizeW(this, Anchors[Location.West]));
                    Anchors[Location.NorthWest].Handles.Add(new SelectionFrameHandleSizeNW(this, Anchors[Location.NorthWest]));
                    
                    Anchors[Location.South].Handles.Add(new SelectionFrameHandleMove(this, Anchors[Location.South]));

                    break;
                case Toolset.Rotation:
                    // Wechsel zu Shearing
                    CurrentToolset = Toolset.Shearing;

                    //Anchors.Clear();

                    break;
                case Toolset.Shearing:
                    // Wechsel zu Sizing
                    CurrentToolset = Toolset.Sizing;

                    //Anchors.Clear();
                    //Anchors.Add(new SelectionFrameHandleAnchorShearTop(this));
                    //Anchors.Add(new SelectionFrameHandleAnchorShearBottom(this));
                    //Anchors.Add(new SelectionFrameHandleShearLeft(this));
                    //Anchors.Add(new SelectionFrameHandleShearRight(this));

                    break;
            }
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
            return base.GetMatrix(context, anchors, Frame);
        }
    }
}
