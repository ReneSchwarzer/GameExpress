﻿using GameExpress.Dialog;
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
        public TimeLinePanel()
        {
            this.InitializeComponent();
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

                // Prüfe, ob zeit außerhalb des sichtbaren bereiches ist oder kurz davor ist
                if (Time > TimeOffset + Ruler.ActualWidth - 45)
                {
                    var dif = Time - TimeOffset - Ruler.ActualWidth + 90;

                    if (dif >= 45)
                    {
                        TimeOffset += (ulong)dif;
                    }
                }
                else if (Time == 0)
                {
                    TimeOffset = 0;
                }

                TimePosition.SetValue(Canvas.LeftProperty, (double)(Time - TimeOffset));
                TimePositionLocator.SetValue(Canvas.LeftProperty, (double)(Time - TimeOffset));
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

            Item.StoryBoard.CollectionChanged += OnInstancesCollectionChanged;
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
            Item.StoryBoard.CollectionChanged -= OnInstancesCollectionChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control seine Größe ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = Table.Columns.Take(2).Sum(x => x.ActualWidth);
            Canvas.SetLeft(Group, width);

            width = ActualWidth - width;

            Group.Width = width > 1 ? width : 1;
            Ruler.Width = width > 1 ? width : 1;
            Header.Width = ActualWidth;
            TimePositionLocator.Height = ActualHeight;

            TimePositionLocator.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, ActualWidth, ActualHeight)
            };
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
        /// Liefert den VisualTree ab dem Element in preorder
        /// </summary>
        /// <param name="element">Begin des </param>
        /// <returns>Eine Liste in preorder des VisualTrees</returns>
        private ICollection<DependencyObject> GetVisualTreePreorder(DependencyObject element)
        {
            var list = new List<DependencyObject>();

            list.Add(element);

            var childsCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childsCount; i++)
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
        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Zeigegerät bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnRulerPointerMoved(CoreWindow sender, PointerEventArgs args)
        {

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
            }

            e.Handled = true;

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Zeigegerät über das Lineal bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(Ruler);
            if (pointer.IsInContact)
            {
                Time = (ulong)pointer.Position.X + TimeOffset;
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
            var lightGray = Color.FromArgb(255, 125, 125, 125);
            var black = Color.FromArgb(255, 0, 0, 0);
            var accent = new UISettings().GetColorValue(UIColorType.AccentDark3);
            ulong count = TimeOffset;

            for (int i = 0; i < Ruler.ActualWidth; i += 10)
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
        }
        /// <summary>
        /// Wird aufgerufen, wenn die zugehörige Instanz geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnChangeInstance(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as MenuFlyoutItem;
            if (element == null) return;

            var story = Item?.StoryBoard?.Where(x => x.ID.Equals(element.Tag))?.FirstOrDefault();
            if (story == null) return;

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
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
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
            get { return (ItemAnimation)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
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
            get { return (ulong)GetValue(TimeOffsetProperty); }
            set { SetValue(TimeOffsetProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TimeOffset.
        /// </summary>
        public static readonly DependencyProperty TimeOffsetProperty =
            DependencyProperty.Register("TimeOffset", typeof(ulong), typeof(TimeLinePanel), new PropertyMetadata(new ulong()));


    }
}
