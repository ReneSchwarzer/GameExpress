using GameExpress.Dialog;
using GameExpress.Model.Item;
using GameExpress.View;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.Controls
{
    public sealed partial class TimeLinePanel : UserControl
    {
        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die Derigistrierung benötigt wird
        /// </summary>
        private long TimePropertyToken { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die Derigistrierung benötigt wird
        /// </summary>
        private long TimeOffsetPropertyToken { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die Derigistrierung benötigt wird
        /// </summary>
        private long ItemToken { get; set; }

        /// <summary>
        /// Token, welches beim RegisterPropertyChangedCallback erzeugt und für die Derigistrierung benötigt wird
        /// </summary>
        //private long SelectedItemsToken { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TimeLinePanel()
        {
            SelectedItems = new ObservableCollection<Item>();
            InitializeComponent();
            Time = 0;

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
                Ruler?.Invalidate();

                // Zeiten aktualisieren
                var childs = GetVisualTreePreorder(this).Where(x => x is KeyFrameEditor);
                foreach (var c in childs)
                {
                    (c as KeyFrameEditor).Time = Time;
                }

                // Prüfe, ob Zeit außerhalb des sichtbaren Bereiches ist oder kurz davor ist
                if (Time > TimeOffset + Ruler.ActualWidth - 45)
                {
                    var lamda = Time - (TimeOffset + Ruler.ActualWidth - 45);

                    if (lamda >= 1)
                    {
                        TimeOffset += (ulong)lamda;
                    }
                }
                else if (Time < TimeOffset + 45)
                {
                    var lamda = 45 - (Time - TimeOffset);

                    if (lamda >= 1)
                    {
                        if ((decimal)TimeOffset - lamda > 1)
                        {
                            TimeOffset -= lamda;
                        }
                        else
                        {
                            TimeOffset = 0;
                        }
                    }
                }
                else if (Time == 0)
                {
                    TimeOffset = 0;
                }

                TimePosition.SetValue(Canvas.LeftProperty, Header.ActualWidth + Time - TimeOffset);
                TimePositionLocator.SetValue(Canvas.LeftProperty, Header.ActualWidth + Time - TimeOffset);
            }));

            // Eigenschaft TimeOffsetProperty hat sich geändert
            TimeOffsetPropertyToken = RegisterPropertyChangedCallback(TimeOffsetProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                // Neuzeichnen erforderlich
                Ruler?.Invalidate();

                // Zeiten aktualisieren
                var childs = GetVisualTreePreorder(this).Where(x => x is KeyFrameEditor);
                foreach (var c in childs)
                {
                    (c as KeyFrameEditor).TimeOffset = TimeOffset;
                }

            }));

            ItemToken = RegisterPropertyChangedCallback(ItemProperty, new DependencyPropertyChangedCallback((s, e) =>
            {
                Item.StoryBoard.CollectionChanged += OnInstancesCollectionChanged;
            }));

            //SelectedItemsToken = RegisterPropertyChangedCallback(SelectedItemsProperty, new DependencyPropertyChangedCallback((s, e) =>
            //{
            //    SelectedItems.CollectionChanged += OnSelectedItemsCollectionChanged;
            //}));

            SelectedItems.CollectionChanged += OnSelectedItemsCollectionChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs args)
        {
            UnregisterPropertyChangedCallback(TimeProperty, TimePropertyToken);
            UnregisterPropertyChangedCallback(TimeOffsetProperty, TimeOffsetPropertyToken);
            UnregisterPropertyChangedCallback(ItemProperty, ItemToken);
            //UnregisterPropertyChangedCallback(SelectedItemsProperty, SelectedItemsToken);
            if (Item != null)
            {
                Item.StoryBoard.CollectionChanged -= OnInstancesCollectionChanged;
            }

            SelectedItems.CollectionChanged -= OnSelectedItemsCollectionChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control seine Größe ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid.Width = ActualWidth;
            TimePositionLocator.Height = ActualHeight;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Instanz-Auflistung geändert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnInstancesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Instanz-Auflistung geändert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Table.SelectedItems.Clear();
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Item add in e.NewItems)
                {
                    // Bereits vorhanden
                    var count = Table.SelectedItems.Where(x => x == add).Count();

                    if (count == 0)
                    {
                        Table.SelectedItems.Add(add);
                    }

                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Item remove in e.OldItems)
                {
                    var items = Table.SelectedItems.Where(x => x == remove);
                    foreach (var item in items.ToList())
                    {
                        Table.SelectedItems.Remove(item);
                    }
                }
            }

        }

        /// <summary>
        /// Liefert den VisualTree ab dem Element in preorder
        /// </summary>
        /// <param name="element">Begin des </param>
        /// <returns>Eine Liste in preorder des VisualTrees</returns>
        private ICollection<DependencyObject> GetVisualTreePreorder(DependencyObject element)
        {
            var list = new List<DependencyObject>
            {
                element
            };

            var childsCount = VisualTreeHelper.GetChildrenCount(element);
            for (var i = 0; i < childsCount; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                if (child != null)
                {
                    list.AddRange(GetVisualTreePreorder(child));
                }
            }

            return list;
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Zeigegerät, was vorher gedrückt wurde, losgelassen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRulerPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Ruler.ReleasePointerCapture(e.Pointer);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät über das Lineal gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRulerPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Ruler);
            if (pointer.IsInContact)
            {
                Time = (ulong)pointer.Position.X + TimeOffset;
                Ruler.CapturePointer(e.Pointer);
            }

            e.Handled = true;

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät über das Lineal bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRulerPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Ruler);
            if (pointer.IsInContact)
            {
                if (pointer.Position.X + TimeOffset > 0)
                {
                    Time = (ulong)pointer.Position.X + TimeOffset;
                }
                else
                {
                    Time = 0;
                }

            }

            e.Handled = true;
        }

        /// <summary>
        /// Zeichnet das Lineal
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDrawRuler(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var white = Color.FromArgb(255, 255, 255, 255);
            var lightGray = (Color)Application.Current.Resources["SystemChromeHighColor"];
            var black = Color.FromArgb(255, 0, 0, 0);
            var accent = new UISettings().GetColorValue(UIColorType.AccentDark3);
            var count = TimeOffset / 10;

            args.DrawingSession.FillRectangle(new Rect(0, 0, Ruler.ActualWidth, Ruler.ActualHeight), lightGray);

            for (var i = 0f - TimeOffset % 10; i < Ruler.ActualWidth; i += 10)
            {
                if (count % 10 == 0)
                {
                    args.DrawingSession.DrawLine(i, 3, i, (float)Ruler.ActualHeight - 4, white);
                }
                else
                {
                    args.DrawingSession.DrawLine(i, 8, i, (float)Ruler.ActualHeight - 8, white);
                }

                count++;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Auswahl in der Tabelle geändert wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewHelper.ChangePropertyPage(e.AddedItems.FirstOrDefault() as Item);

            foreach (var add in e.AddedItems.ToList())
            {
                // Bereits vorhanden
                var count = SelectedItems.Where(x => x == add).Count();

                if (count == 0)
                {
                    SelectedItems.Add(add as Item);
                }
            }

            foreach (var remove in e.RemovedItems.ToList())
            {
                SelectedItems.Remove(remove as Item);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein neuer KeyFrame hinzugefügt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddKeyFrame(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as MenuFlyoutItem;
            if (element == null)
            {
                return;
            }

            var story = Item?.StoryBoard?.Where(x => x.ID.Equals(element.Tag))?.FirstOrDefault();
            if (story == null)
            {
                return;
            }

            if (story.KeyFrames.Count > 0)
            {
                story.KeyFrames.Add(new ItemKeyFrameTweening() { });
            }

            story.KeyFrames.Add(new ItemKeyFrameAct() { From = Time, Duration = 500 });
        }

        /// <summary>
        /// Wird aufgerufen, wenn die zugehörige Instanz geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnChangeInstance(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as MenuFlyoutItem;
            if (element == null)
            {
                return;
            }

            var story = Item?.StoryBoard?.Where(x => x.ID.Equals(element.Tag))?.FirstOrDefault();
            if (story == null)
            {
                return;
            }

            var dialog = new SelectInstanceDialog()
            {
                CurrentItem = Item,
                SelectedItem = story.Instance
            };

            switch (await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    {
                        story.Item = dialog.SelectedItem?.Name;

                    }
                    break;
            }

        }

        /// <summary>
        /// Wird aufgerufen, wenn eine Story gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnDeleteStory(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Möchten Sie die Story wirklich löschen?", "Löschen");
            var yesCommand = new UICommand("Ja");
            var noCommand = new UICommand("Nein");
            dialog.Commands.Add(yesCommand);
            dialog.Commands.Add(noCommand);
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 1;

            var command = await dialog.ShowAsync();
            if (command == yesCommand)
            {
                var element = e.OriginalSource as MenuFlyoutItem;

                if (element != null)
                {
                    var story = Item.StoryBoard.Where(x => x.ID.Equals(element.Tag));
                    Item.StoryBoard.Remove(story.FirstOrDefault());
                }
            }
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
        /// Using a DependencyProperty as the backing store for Time.
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(new ulong()));

        /// <summary>
        /// Liefert oder setzt das Items
        /// </summary>
        public ItemAnimation Item
        {
            get => (ItemAnimation)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ItemAnimation), typeof(TimeLinePanel), new PropertyMetadata(null));

        /// <summary>
        /// Liefert oder setzt den Offset der Animationszeit
        /// </summary>
        public ulong TimeOffset
        {
            get => (ulong)GetValue(TimeOffsetProperty);
            set => SetValue(TimeOffsetProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TimeOffset.
        /// </summary>
        public static readonly DependencyProperty TimeOffsetProperty =
            DependencyProperty.Register("TimeOffset", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(new ulong()));

        /// <summary>
        /// Liefert oder setzt die Liste der ausgewählten Items
        /// </summary>
        public ObservableCollection<Item> SelectedItems
        {
            get => (ObservableCollection<Item>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<Item>), typeof(TimeLinePanel), new PropertyMetadata(null));
    }
}
