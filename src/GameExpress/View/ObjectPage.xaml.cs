using GameExpress.Model.Item;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Ansichtsseite eines Objektes
    /// </summary>
    public sealed partial class ObjectPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Objekt
        /// </summary>
        private ItemObject Object => DataContext as ItemObject;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ObjectPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zwingt das Control zum neuzeichnen
        /// </summary>
        public void Invalidate()
        {
            Editor.Invalidate();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            DataContext = args.Parameter as ItemObject;
            ViewHelper.ChangePropertyPage(args.Parameter as Item);

            Editor.FitSize();
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neues Objekt erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddObject(object sender, RoutedEventArgs args)
        {
            Object.Children.Add(new ItemObject() { Name = "Neues Objekt" });
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Animation erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddAnimation(object sender, RoutedEventArgs args)
        {
            Object.Children.Add(new ItemAnimation() { Name = "Neue Animation" });
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Karte erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddMap(object sender, RoutedEventArgs args)
        {
            Object.Children.Add(new ItemMap() { Name = "Neue Karte" });
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neues Bild erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddImage(object sender, RoutedEventArgs args)
        {
            Object.Children.Add(new ItemImage() { Name = "Neues Bild" });
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neuer Sound erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddSound(object sender, RoutedEventArgs args)
        {
            Object.Children.Add(new ItemSound() { Name = "Neuer Sound" });
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Previousbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPrevious(object sender, RoutedEventArgs args)
        {
            Play.IsChecked = false;
            Editor.Time = 0;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Playbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPlay(object sender, RoutedEventArgs args)
        {
            var isChecked = Play.IsChecked.Value;
            var ticks = DateTime.Now.Ticks;

            Task.Run(async () =>
            {
                while (isChecked)
                {
                    var now = DateTime.Now.Ticks;
                    var delta = (ulong)(now - ticks) / 10000;

                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        Editor.Time = delta;
                        Editor.Invalidate();
                        isChecked = Play.IsChecked.Value;
                    });

                    Thread.Sleep(20);
                }
            });

        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Auswahl des Zustandes (Animation) geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnStateSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Editor.Invalidate();
        }
    }
}
