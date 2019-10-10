using GameExpress.Dialog;
using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Eigenschaftsseite eines Objektzustandes
    /// </summary>
    public sealed partial class AnimationPropertyPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Zustand
        /// </summary>
        private ItemAnimation Animation => DataContext as ItemAnimation;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AnimationPropertyPage()
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
        /// Wird aufgerufen, wenn eine Objekt gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnDeleteState(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Möchten Sie den Zustand wirklich löschen?", "Löschen");
            var yesCommand = new UICommand("Ja");
            var noCommand = new UICommand("Nein");
            dialog.Commands.Add(yesCommand);
            dialog.Commands.Add(noCommand);
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 1;

            var command = await dialog.ShowAsync();
            if (command == yesCommand)
            {
                var parent = Animation.Parent;
                parent.Children.Remove(Animation);

                ViewHelper.ChangePropertyPage(parent);
                ViewHelper.ChangePage(parent);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der zugehörige Hintergrund geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnChangeBackground(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as Button;
            if (element == null)
            {
                return;
            }

            var dialog = new SelectInstanceDialog()
            {
                //CurrentItem = Animation.Background,
                SelectedItem = Animation.Background.Instance
            };

            switch (await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    {
                        Animation.Background.ID = dialog.SelectedItem?.ID;
                        Animation.Background.Name = dialog.SelectedItem?.Name;
                    }
                    break;
            }

        }
    }
}
