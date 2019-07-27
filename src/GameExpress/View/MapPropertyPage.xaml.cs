using GameExpress.Model.Item;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Eigenschaftsseite einer Karte
    /// </summary>
    public sealed partial class MapPropertyPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Karte
        /// </summary>
        private ItemMap Map => DataContext as ItemMap;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MapPropertyPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = e.Parameter;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Karte gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnDeleteMap(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Möchten Sie die Karte wirklich löschen?", "Löschen");
            var yesCommand = new UICommand("Ja");
            var noCommand = new UICommand("Nein");
            dialog.Commands.Add(yesCommand);
            dialog.Commands.Add(noCommand);
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 1;

            var command = await dialog.ShowAsync();
            if (command == yesCommand)
            {
                var parent = Map.Parent;
                parent.Children.Remove(Map);

                ViewHelper.ChangePropertyPage(parent);
                ViewHelper.ChangePage(parent);
            }
        }
    }
}
