using GameExpress.Dialog;
using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        private ItemStory Story { get { return DataContext as ItemStory; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public StoryPropertyPage()
        {
            this.InitializeComponent();
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
        /// Wird aufgerufen, wenn die zugehörige Instanz geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnChangeInstance(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as Button;
            if (element == null) return;

            var dialog = new SelectInstanceDialog()
            {
                CurrentItem = Story.Object as ItemAnimation,
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
    }
}
