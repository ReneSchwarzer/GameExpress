﻿using GameExpress.Model.Item;
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
    /// Eigenschaftsseite eines Sounditems
    /// </summary>
    public sealed partial class SoundPropertyPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Karte
        /// </summary>
        private ItemSound Sound => DataContext as ItemSound;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SoundPropertyPage()
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
        /// Wird aufgerufen, wenn ein Sound gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnDeleteSound(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Möchten Sie den Sound wirklich löschen?", "Löschen");
            var yesCommand = new UICommand("Ja");
            var noCommand = new UICommand("Nein");
            dialog.Commands.Add(yesCommand);
            dialog.Commands.Add(noCommand);
            dialog.DefaultCommandIndex = 1;
            dialog.CancelCommandIndex = 1;

            var command = await dialog.ShowAsync();
            if (command == yesCommand)
            {
                var parent = Sound.Parent;
                parent.Children.Remove(Sound);

                ViewHelper.ChangePropertyPage(parent);
                ViewHelper.ChangePage(parent);
            }
        }
    }
}
