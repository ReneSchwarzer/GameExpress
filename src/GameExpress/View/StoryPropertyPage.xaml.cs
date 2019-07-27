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
    /// Eiegnschaftsseite einer Story
    /// </summary>
    public sealed partial class StoryPropertyPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Story
        /// </summary>
        private ItemStory Story => DataContext as ItemStory;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public StoryPropertyPage()
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

            Loop.ItemsSource = new string[] { "Ohne", "Einfrieren", "Einfach", "Pendeln" };

            switch (Story.Loop)
            {
                case Model.Structs.Loop.Freeze:
                    Loop.SelectedValue = "Einfrieren";
                    break;
                case Model.Structs.Loop.Repeat:
                    Loop.SelectedValue = "Einfach";
                    break;
                case Model.Structs.Loop.Oscillate:
                    Loop.SelectedValue = "Pendeln";
                    break;
                default:
                    Loop.SelectedValue = "Ohne";
                    break;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die zugehörige Instanz geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnChangeInstance(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as Button;
            if (element == null)
            {
                return;
            }

            var dialog = new SelectInstanceDialog()
            {
                CurrentItem = Story.Animation as ItemAnimation,
                SelectedItem = Story.Instance
            };

            switch (await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    {
                        Story.Item = dialog.SelectedItem?.Name;

                    }
                    break;
            }

        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Wiederholungsauswahl ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnLoopSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedValue)
            {
                case "Einfrieren":
                    Story.Loop = Model.Structs.Loop.Freeze;
                    break;
                case "Einfach":
                    Story.Loop = Model.Structs.Loop.Repeat;
                    break;
                case "Pendeln":
                    Story.Loop = Model.Structs.Loop.Oscillate;
                    break;
                default:
                    Story.Loop = Model.Structs.Loop.None;
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
                var obj = Story.Animation;
                obj.StoryBoard.Remove(Story);

                ViewHelper.ChangePropertyPage(obj);
            }
        }
    }
}
