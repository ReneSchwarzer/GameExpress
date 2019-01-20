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
        /// Liefert oder setzt das ausgewählte Schlüsselbild
        /// </summary>
        private SelectionHelper<ItemKeyFrameAct> SelectedKeyFrame { get; set; }

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
        /// Liefert die fensterbezogenen Koordinaten der KeyFrames
        /// </summary>
        /// <returns>Eine Liste der Keyframes mit fensterbezogenen Koordinaten</returns>
        private ICollection<Tuple<float, float, ItemKeyFrame>> GetWindowCoordinates()
        {
            var list = new List<Tuple<float, float, ItemKeyFrame>>();
            {
                var absoluteTime = (float)0;
                var predecessorTweening = (Tuple<float, float, ItemKeyFrame>)null;

                foreach (var k in Story?.KeyFrames)
                {
                    if (k is ItemKeyFrameAct act)
                    {
                        if (predecessorTweening != null)
                        {
                            predecessorTweening = new Tuple<float, float, ItemKeyFrame>
                            (
                                predecessorTweening.Item1,
                                absoluteTime + act.From - TimeOffset,
                                predecessorTweening.Item3
                            );

                            list.Add(predecessorTweening);
                            predecessorTweening = null;
                        }

                        // Handlung
                        var item = new Tuple<float, float, ItemKeyFrame>
                        (
                            absoluteTime + act.From - TimeOffset,
                            absoluteTime + act.From + act.Duration - TimeOffset,
                            act
                        );

                        absoluteTime += act.From + act.Duration;
                        list.Add(item);
                    }
                    else if (k is ItemKeyFrameTweening tweening)
                    {
                        // Tweening
                        predecessorTweening = new Tuple<float, float, ItemKeyFrame>
                        (
                            absoluteTime - TimeOffset,
                            0,
                            tweening
                        );
                    }
                }
            }

            return list;
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
            if (Story != null)
            {
                Story.KeyFrames.CollectionChanged += OnCollectionChanged;
                Story.PropertyChanged += OnKeyFramePropertyChanged;
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

            if (Story != null)
            {
                Story.KeyFrames.CollectionChanged -= OnCollectionChanged;
                Story.PropertyChanged -= OnKeyFramePropertyChanged;
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
                foreach (ItemKeyFrame v in args.OldItems)
                {
                    v.Parent = null;
                }
            }

            Invalidate();
        }

        /// <summary>
        /// Zeichnet das Die Schlüsselbilder
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var lightGray = (Color)Application.Current.Resources["SystemChromeLowColor"];
            var accent3 = new UISettings().GetColorValue(UIColorType.AccentLight3);
            var accent1 = new UISettings().GetColorValue(UIColorType.AccentLight1);
            var accentDark = new UISettings().GetColorValue(UIColorType.AccentDark1);

            // Hintergrundgitter
            for (int j = 0; j < ActualWidth; j += 10)
            {
                var x = (float)TimeOffset % 10;
                args.DrawingSession.DrawLine(j - x, 0, j - x, (float)ActualHeight, lightGray);
            }

            // Fenster Koordinaten ermitteln
            var list = GetWindowCoordinates();

            // Zeichnen
            foreach (var item in list)
            {
                if (item.Item2 < 0) continue;
                else if (item.Item1 > Content.ActualWidth) break;

                if (item.Item3 is ItemKeyFrameAct act)
                {
                    // Handlung
                    if (SelectedKeyFrame != null && SelectedKeyFrame.Item == item.Item3)
                    {
                        if (SelectedKeyFrame.Outside)
                        {
                            var red = Color.FromArgb(125, 255, 10, 10);
                            args.DrawingSession.FillRectangle(item.Item1, 0, act.Duration, (float)ActualHeight, red);
                        }
                        else
                        {
                            var accent2 = new UISettings().GetColorValue(UIColorType.AccentLight2);
                            accent2.A = 125;
                            args.DrawingSession.FillRectangle(item.Item1, 0, act.Duration, (float)ActualHeight, accent2);
                        }
                    }
                    else
                    {
                        args.DrawingSession.FillRectangle(item.Item1, 0, act.Duration, (float)ActualHeight, accent3);
                    }

                    args.DrawingSession.DrawRectangle(item.Item1, 0, act.Duration, (float)ActualHeight, accent1);

                    if (act.Duration > 15)
                    {
                        args.DrawingSession.FillRectangle(item.Item1, 0, 10, 10, accentDark);
                        args.DrawingSession.FillRectangle(item.Item2, (float)ActualHeight, -10, -10, accentDark);
                    }
                }
                else if (item.Item3 is ItemKeyFrameTweening tweening && item.Item3.Enable)
                {
                    // Tweening
                    args.DrawingSession.DrawLine
                    (
                        (float)item.Item1,
                        (float)(ActualHeight / 2),
                        (float)item.Item2,
                        (float)(ActualHeight / 2),
                        accent1
                    );

                    args.DrawingSession.FillEllipse
                    (
                        (float)item.Item1,
                        (float)(ActualHeight / 2),
                        4,
                        4,
                        accent1
                    );

                    args.DrawingSession.DrawLine
                    (
                        (float)item.Item2 - 4,
                        (float)(ActualHeight / 2) - 3,
                        (float)item.Item2,
                        (float)(ActualHeight / 2),
                        accent1
                    );
                    args.DrawingSession.DrawLine
                    (
                        (float)item.Item2 - 4,
                        (float)(ActualHeight / 2) + 3,
                        (float)item.Item2,
                        (float)(ActualHeight / 2),
                        accent1
                    );
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn auf das Steuerelement gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pointer = e.GetCurrentPoint(this);
            var x = pointer.Position.X;
            var y = pointer.Position.Y;

            e.Handled = true;

            // Story ist für Bearbeitung gesperrt
            if (Story.Lock) return;

            // Fenster Koordinaten ermitteln
            var list = GetWindowCoordinates();

            // Zeichnen
            foreach (var item in list)
            {
                if (item.Item2 < 0) continue;
                else if (item.Item1 > Content.ActualWidth) break;

                if (x > item.Item1 && x < item.Item2)
                {
                    if (item.Item3 is ItemKeyFrameAct act)
                    {
                        ViewHelper.ChangePropertyPage(act);

                        if (act.Lock) return;

                        Content.CapturePointer(e.Pointer);
                        SelectedKeyFrame = new SelectionHelper<ItemKeyFrameAct>()
                        {
                            Item = act,
                            OriginalPosition = pointer.Position,
                            OriginalItemPosition = new Point(act.From, act.Duration)
                        };

                        if (x <= item.Item1 + 10 && y < 10)
                        {
                            SelectedKeyFrame.EditMode = SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.From;
                        }
                        else if (x >= item.Item2 - 10 && y > ActualHeight - 10)
                        {
                            SelectedKeyFrame.EditMode = SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.Duration;
                        }
                        else
                        {
                            SelectedKeyFrame.EditMode = SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.Move;
                        }

                    }
                    else if (item.Item3 is ItemKeyFrameTweening tweening)
                    {
                        ViewHelper.ChangePropertyPage(tweening);
                    }

                    return;
                }
            }

            ViewHelper.ChangePropertyPage(Story);
        }

        /// <summary>
        /// Wird aufgerufen, wenn innerhalb des Steuerelements die Position des Zeigegerätes sich ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;

            var pointer = e.GetCurrentPoint(this);

            if (SelectedKeyFrame != null )
            {
                var delta = pointer.Position.X - SelectedKeyFrame.OriginalPosition.X;
                var value = SelectedKeyFrame.OriginalItemPosition.X + delta;

                if (value < 0)
                {
                    value = 0;
                }

                // Prüfe, ob im aktuellen Steuerelement
                SelectedKeyFrame.Outside = !(pointer.Position.X >= 0 && pointer.Position.X <= ActualWidth &&
                                             pointer.Position.Y >= 0 && pointer.Position.Y <= ActualHeight);

                if (SelectedKeyFrame.EditMode == SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.Move)
                {
                    // Verschieben
                    SelectedKeyFrame.Item.From = (ulong)Math.Abs(value);
                }
                else if (SelectedKeyFrame.EditMode == SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.From)
                {
                    var duration = SelectedKeyFrame.Item.From + SelectedKeyFrame.Item.Duration;
                    
                    // Größe Ändern
                    SelectedKeyFrame.Item.From = (ulong)Math.Abs(value);
                    if ((double)duration - SelectedKeyFrame.Item.From > 2)
                    {
                        SelectedKeyFrame.Item.Duration = duration - SelectedKeyFrame.Item.From;
                    }
                    else
                    {
                        SelectedKeyFrame.Item.Duration = 1;
                    }
                }
                else if (SelectedKeyFrame.EditMode == SelectionHelper<ItemKeyFrameAct>.SelectionEditMode.Duration)
                {
                    // Größe Ändern
                    if (SelectedKeyFrame.OriginalItemPosition.Y + delta > 2f)
                    {
                        SelectedKeyFrame.Item.Duration = (ulong)(SelectedKeyFrame.OriginalItemPosition.Y + delta);
                    }
                    else
                    {
                        SelectedKeyFrame.Item.Duration = 1;
                    }
                }

                Invalidate();
            }

        }

        /// <summary>
        /// Wird aufgerufen, wenn das Zeigegeräte nicht mehr gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;

            var pointer = e.GetCurrentPoint(this);

            if (SelectedKeyFrame != null)
            {
                // Prüfe, ob im aktuellen Steuerelement
                if (!(pointer.Position.X >= 0 &&  pointer.Position.X <= ActualWidth &&
                      pointer.Position.Y >= 0 && pointer.Position.Y <= ActualHeight))
                {
                    // Orginalposition wiederherstellen
                    SelectedKeyFrame.Item.From = (ulong)SelectedKeyFrame.OriginalItemPosition.X;
                    SelectedKeyFrame.Item.Duration = (ulong)SelectedKeyFrame.OriginalItemPosition.Y;
                }

                SelectedKeyFrame = null;

                Invalidate();

                Content.ReleasePointerCapture(e.Pointer);
            }

        }
        
        /// <summary>
        /// Wird aufgerufen, wenn das Zeigegeräte aus dem Steuerelement bewegt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn das Zeigegeräte nicht mehr gültig ist
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPointerCanceled(object sender, PointerRoutedEventArgs e)
        {
           
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich eine Story geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnKeyFramePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Invalidate();
        }

        /// <summary>
        /// Liefert oder setzt die KeyFrames
        /// </summary>
        public ItemStory Story
        { 
            get { return (ItemStory)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for KeyFrames.
        /// </summary>
        public static readonly DependencyProperty StoryProperty =
            DependencyProperty.Register("Story", typeof(ItemStory), typeof(KeyFrameEditor), new PropertyMetadata(null));

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
