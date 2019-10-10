﻿using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using GameExpress.View;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace GameExpress.Controls
{
    /// <summary>
    /// Editor für animierte Objekte
    /// </summary>
    public class AnimationEditorPanel : EditorPanel
    {
        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die Derigistrierung  benötigt wird
        /// </summary>
        private long TimePropertyToken { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AnimationEditorPanel()
        {
            DefaultStyleKey = typeof(EditorPanel);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            PointerPressed += OnPointerPressed;
        }

        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Time = 0;
        }

        /// <summary>
        /// Erstellt die Ressourcen
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnCreateResources(CanvasCreateResourcesEventArgs args)
        {
            Parallel.Invoke(() => { Item?.CreateResources(Content); });
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            // Eigenschaft TimeProperty hat sich geändert
            TimePropertyToken = RegisterPropertyChangedCallback(TimeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Invalidate();
            }));
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs args)
        {
            UnregisterPropertyChangedCallback(TimeProperty, TimePropertyToken);
        }

        /// <summary>
        /// Zeichnet das Item
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnDrawContent(CanvasDrawEventArgs args)
        {
            var viewRect = GetItemViewRect(out var infinty);

            // Zeichne Hintergrund
            OnDrawBackground(args);

            var uc = new UpdateContext()
            {
                Designer = true,
                Time = new Time(Time),
                Matrix = Matrix3D.Identity * Matrix3D.Translation(viewRect.Left, viewRect.Top) * Matrix3D.Scaling(Zoom / 100f, Zoom / 100f)
            };

            var pc = new PresentationContext(args.DrawingSession)
            {
                Designer = true,
                Time = new Time(Time),
                Matrix = Matrix3D.Identity * Matrix3D.Translation(viewRect.Left, viewRect.Top) * Matrix3D.Scaling(Zoom / 100f, Zoom / 100f)
            };

            Item.Update(uc);
            Item.Presentation(pc);

            // SelectionFrame updaten
            foreach (var frame in SelectionFrames)
            {
                frame.Update(uc);
            }

            // SelectionFrame zeichnen
            foreach (var frame in SelectionFrames)
            {
                frame.Presentation(pc);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ViewHelper.ChangePropertyPage(Item);
        }

        /// <summary>
        /// Erzeugt einen HitTestContext
        /// </summary>
        /// <param name="viewRect">Die Koordinaten des Bereiches indem das Item gezeichnet wird</param>
        /// <returns>Der HitTestContext</returns>
        protected override HitTestContext CrateHitTestContext(Rect viewRect)
        {
            var hc = base.CrateHitTestContext(viewRect);
            hc.Time = new Time(Time);

            return hc;
        }

        /// <summary>
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get => (ulong)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for HorizontalScrollValue.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(ulong), typeof(AnimationEditorPanel), new PropertyMetadata(new ulong()));

    }
}
