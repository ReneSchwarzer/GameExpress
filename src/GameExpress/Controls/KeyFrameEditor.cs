using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace GameExpress.Controls
{
    /// <summary>
    /// Editor zum Anzeigen und bearbeiten der KeyFrames
    /// </summary>
    public sealed class KeyFrameEditor : Control
    {
        /// <summary>
        /// Liefert oder setzt die Ansicht
        /// </summary>
        protected Canvas Content { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public KeyFrameEditor()
        {
            this.DefaultStyleKey = typeof(KeyFrameEditor);

            Unloaded += OnUnloaded;
        }

        

        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Content = GetTemplateChild("Content") as Canvas;

            if (Content != null)
            {
                Content.SizeChanged += (s, e) =>
                {
                    foreach(UIElement k in Content.Children)
                    {
                        if (k is Rectangle)
                        {
                            (k as Rectangle).Height = Content.ActualHeight;
                        }
                    }
                };
            }

            // Eigenschaft InstancesProperty hat sich geändert
            RegisterPropertyChangedCallback(ItemsProperty, new DependencyPropertyChangedCallback((s, e) =>
            {

            }));

            // Eigenschaft TimeProperty hat sich geändert
            RegisterPropertyChangedCallback(TimeLinePanel.TimeProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Refresh();
            }));

            // Eigenschaften des Items haben sich geändert
            if (Items != null)
            {
                Items.CollectionChanged += OnCollectionChanged;

                Refresh();
            }
        }

        /// <summary>
        /// Aktuallisiert den Inhalt des Steuerelementes
        /// </summary>
        private void Refresh()
        {
            Content.Children.Clear();
            var color = new UISettings();

            foreach(var k in Items)
            {
                var r = new Rectangle()
                {
                    Fill= new SolidColorBrush(color.GetColorValue(UIColorType.AccentLight3)),
                    Width = k.Duration,
                    MinWidth = 10,
                    Height = 25
                    
                };
                r.SetValue(Canvas.TopProperty, 0);
                r.SetValue(Canvas.LeftProperty, k.From);

                Content.Children.Add(r);
            }

            // Zeitmarke
            var time = new Rectangle()
            {
                Fill = new SolidColorBrush(color.GetColorValue(UIColorType.AccentDark1)),
                Width = 6,
                MinWidth = 5,
                MaxWidth = 5,
                Height = 25

            };
            time.SetValue(Canvas.TopProperty, 0);
            time.SetValue(Canvas.LeftProperty, Time - 3);
            time.SetValue(Canvas.ZIndexProperty, Items.Count + 1);

            Content.Children.Add(time);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (Items != null)
            {
                Items.CollectionChanged -= OnCollectionChanged;
            }

            Unloaded -= OnUnloaded;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Aufzählung der KeyFrames sich geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            Refresh();
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

    }
}
