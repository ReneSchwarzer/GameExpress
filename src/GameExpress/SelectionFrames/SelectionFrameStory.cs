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
        private ItemKeyFrame KeyFrame { get; set; }

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
                KeyFrame = frame;

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

                foreach (var handle in Anchors.Values.SelectMany(x => x).SelectMany(x => x.Handles))
                {
                    handle.Item = KeyFrame;
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

            if (Item is ItemStory story && KeyFrame != null)
            {
                var newPC = new PresentationContext(pc)
                {
                    Time = story.LocalTime(pc.Time)
                };

                if (KeyFrame is IItemVisual visual)
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
            if (Item is ItemStory story && KeyFrame != null)
            {
                var newHC = new HitTestContext(hc)
                {
                    Time = story.LocalTime(hc.Time)
                };

                if (KeyFrame is IItemVisual visual)
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
            RemoveHandles(Location.North);
            RemoveHandles(Location.NorthEast);
            RemoveHandles(Location.East);
            RemoveHandles(Location.SouthEast);
            RemoveHandles(Location.South);
            RemoveHandles(Location.SouthWest);
            RemoveHandles(Location.West);
            RemoveHandles(Location.NorthWest);

            switch (CurrentToolset)
            {
                case Toolset.Sizing:
                    // Wechsel zu Rotation
                    CurrentToolset = Toolset.Rotation;

                    AddHandle(Location.North, new SelectionFrameHandleSizeN(this, GetAnchor(Location.North)));
                    AddHandle(Location.NorthEast, new SelectionFrameHandleSizeNE(this, GetAnchor(Location.NorthEast)));
                    AddHandle(Location.East, new SelectionFrameHandleSizeE(this, GetAnchor(Location.East)));
                    AddHandle(Location.SouthEast, new SelectionFrameHandleSizeSE(this, GetAnchor(Location.SouthEast)));
                    AddHandle(Location.South, new SelectionFrameHandleSizeS(this, GetAnchor(Location.South)));
                    AddHandle(Location.SouthWest, new SelectionFrameHandleSizeSW(this, GetAnchor(Location.SouthWest)));
                    AddHandle(Location.West, new SelectionFrameHandleSizeW(this, GetAnchor(Location.West)));
                    AddHandle(Location.NorthWest, new SelectionFrameHandleSizeNW(this, GetAnchor(Location.NorthWest)));

                    AddHandle(Location.South, new SelectionFrameHandleTranslation(this, GetAnchor(Location.South)));

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
            return base.GetMatrix(context, anchors, KeyFrame);
        }
    }
}
