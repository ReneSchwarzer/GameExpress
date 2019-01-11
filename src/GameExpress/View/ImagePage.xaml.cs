using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Ansichtseite des Bildes
    /// </summary>
    public sealed partial class ImagePage : Page
    {
        /// <summary>
        /// Gestenrekorder
        /// </summary>
        private GestureRecognizer Recognizer { get; set; } = new GestureRecognizer();

        /// <summary>
        /// Liefert das mit der Ansicht verbundene Bild
        /// </summary>
        private ItemImage Image {  get { return DataContext as ItemImage; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ImagePage()
        {
            this.InitializeComponent();

            Recognizer.GestureSettings = GestureSettings.Hold | GestureSettings.HoldWithMouse | GestureSettings.ManipulationRotate | GestureSettings.ManipulationRotateInertia | GestureSettings.ManipulationScale | GestureSettings.ManipulationScaleInertia;
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = e.Parameter;

            ViewHelper.ChangePropertyPage(e.Parameter as Item);

            Editor.Item = Image;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite geladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoading(Windows.UI.Xaml.FrameworkElement sender, object args)
        {
            Recognizer.Holding += OnHolding;
            Recognizer.ManipulationCompleted += OnManipulationCompleted;
            //Recognizer.ManipulationInertiaStarting += OnManipulationInertiaStarting;
            //Recognizer.ManipulationStarted += OnManipulationStarted;
            Recognizer.ManipulationUpdated += OnManipulationUpdated;

            Image.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite entladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Recognizer.Holding -= OnHolding;
            Recognizer.ManipulationCompleted -= OnManipulationCompleted;
            //Recognizer.ManipulationInertiaStarting -= OnManipulationInertiaStarting;
            //Recognizer.ManipulationStarted -= OnManipulationStarted;
            Recognizer.ManipulationUpdated -= OnManipulationUpdated;

            Image.PropertyChanged -= OnPropertyChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Eigenschaften des Bildes geändert haben
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Editor.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnManipulationUpdated(GestureRecognizer sender, ManipulationUpdatedEventArgs args)
        {
            var m = args.Cumulative;
            if (m.Expansion != 0)
            {
                int zoom = (int)m.Expansion;

                if (zoom < 10)
                {
                    zoom = 10;
                }
                else if (zoom > 200)
                {
                    zoom = 200;
                }

                //ZoomSlider.Value = zoom;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender">Der Auslöser des Events</param>
        ///// <param name="args">Das Eventargument</param>
        //private void OnManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        //{

        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender">Der Auslöser des Events</param>
        ///// <param name="args">Das Eventargument</param>
        //private void OnManipulationInertiaStarting(GestureRecognizer sender, ManipulationInertiaStartingEventArgs args)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {
            var m = args.Cumulative;
            if (m.Expansion > 0)
            {
                //textBoxGesture.Text = "Zoom gesture detected";

                
            }
            else if (m.Expansion < 0)
            {
                //textBoxGesture.Text = "Pinch gesture detected";
            }

            if (m.Rotation != 0.0)
            {
                //textBoxGesture.Text = "Rotation detected";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnHolding(GestureRecognizer sender, HoldingEventArgs args)
        {
            string holdingState = "";
            switch (args.HoldingState)
            {
                case HoldingState.Canceled:
                    holdingState = "Cancelled";
                    break;
                case HoldingState.Completed:
                    holdingState = "Completed";
                    break;
                case HoldingState.Started:
                    holdingState = "Started";
                    break;
            }
            //textBoxGesture.Text = "Holding state = " + holdingState;
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs args)
        {
            //Recognizer.ProcessDownEvent(args.GetCurrentPoint(this));
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs args)
        {
            //Recognizer.ProcessMoveEvents(args.GetIntermediatePoints(this));
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs args)
        {
            //Recognizer.ProcessUpEvent(args.GetCurrentPoint(this));
        }

        void OnPointerCanceled(object sender, PointerRoutedEventArgs args)
        {
            //Recognizer.CompleteGesture();
        }
    }
}
