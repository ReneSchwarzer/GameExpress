using GameExpress.Model.Item;
using GameExpress.View;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace GameExpress.Controls
{
    /// <summary>
    /// Editor zum Anzeigen und Bearbeiten der KeyFrames
    /// </summary>
    public sealed partial class KeyFrameEditor : UserControl
    {
        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die derigistrierung benötigt wird
        /// </summary>
        private long TimePropertyToken { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die derigistrierung benötigt wird
        /// </summary>
        private long TimeOffsetPropertyToken { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public KeyFrameEditor()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Zwingt das Control zum neuzeichnen
        /// </summary>
        public void Invalidate()
        {
            Content.Invalidate();
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

                // Parent informieren
                var parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is TimeLinePanel))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent != null)
                {
                    (parent as TimeLinePanel).Time = Time;
                }

            }));

            // Eigenschaft TimeOffsetProperty hat sich geändert
            TimeOffsetPropertyToken = RegisterPropertyChangedCallback(TimeOffsetProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Parent informieren
                var parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is TimeLinePanel))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent != null)
                {
                    (parent as TimeLinePanel).TimeOffset = TimeOffset;
                }

            }));

            // Eigenschaften des Items haben sich geändert
            if (Items != null)
            {
                Items.CollectionChanged += OnCollectionChanged;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            UnregisterPropertyChangedCallback(TimeProperty, TimePropertyToken);
            UnregisterPropertyChangedCallback(TimeOffsetProperty, TimeOffsetPropertyToken);

            if (Items != null)
            {
                Items.CollectionChanged -= OnCollectionChanged;
            }

            Unloaded -= OnUnloaded;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control seine Größe ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Content.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, ActualWidth, ActualHeight)
            };
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Aufzählung der KeyFrames sich geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (ItemKeyFrame v in args.NewItems)
                {
                    v.Parent = v;
                }
            }

            if (args.OldItems != null)
            {
                foreach (ItemStory v in args.OldItems)
                {
                    v.Parent = null;
                }
            }

            Invalidate();
        }

        ///// <summary>
        ///// Wird aufgerufen, wenn die Werte eines KeyFrames sich geändert haben
        ///// </summary>
        ///// <param name="sender">Der Auslöser des Events</param>
        ///// <param name="args">Das Eventargument</param>
        //private void OnKeyFrameChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    // Parent informieren
        //    var parent = VisualTreeHelper.GetParent(this);
        //    while (parent != null && !(parent is ObjectPage))
        //    {
        //        parent = VisualTreeHelper.GetParent(parent);
        //    }

        //    if (parent != null)
        //    {
        //        (parent as ObjectPage).Invalidate();
        //    }
        //}

        /// <summary>
        /// Zeichnet das Die Schlüsselbilder
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = Color.FromArgb(255, 125, 125, 125);
            var black = Color.FromArgb(255, 0, 0, 0);
            var accent = new UISettings().GetColorValue(UIColorType.AccentLight3);
            ulong count = TimeOffset;

            foreach (var k in Items)
            {
                args.DrawingSession.FillRectangle(k.From, 0, k.Duration, (float)ActualHeight, accent);
            }
        }

        /// <summary>
        /// Liefert oder setzt die KeyFrames
        /// </summary>
        public ObservableCollection<ItemKeyFrame> Items
        {
            get { return (ObservableCollection<ItemKeyFrame>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for KeyFrames.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<ItemKeyFrame>), typeof(KeyFrameEditor), new PropertyMetadata(new ObservableCollection<ItemKeyFrame>()));

        /// <summary>
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Time.
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(ulong), typeof(KeyFrameEditor), new PropertyMetadata(new ulong()));

        /// <summary>
        /// Liefert oder setzt den Offset der Animationszeit
        /// </summary>
        public ulong TimeOffset
        {
            get { return (ulong)GetValue(TimeOffsetProperty); }
            set { SetValue(TimeOffsetProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TimeOffset.
        /// </summary>
        public static readonly DependencyProperty TimeOffsetProperty =
            DependencyProperty.Register("TimeOffset", typeof(ulong), typeof(KeyFrameEditor), new PropertyMetadata(new ulong()));

    }
}
