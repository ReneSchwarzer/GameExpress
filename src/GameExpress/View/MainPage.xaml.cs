using GameExpress.Model.Item;
using GameExpress.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExpress.View
{
    /// <summary>
    /// Das Hauptfenster
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Liefert oder setzt das Model
        /// </summary>
        public ViewModelMain Model { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainPage()
        {
            Model = new ViewModelMain();

            ViewHelper.MainPage = this;

            DataContext = Model;

            this.InitializeComponent();

            Model.InitAsync();

            ProgressBar.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Wechselt die Eigenschaftsseite
        /// </summary>
        /// <param name="page">Die Seite, zu der gewechselt werden soll</param>
        /// <param name="item">Das Item</param>
        public void ChangePropertyPage(Type page, Item item)
        {
            if (page == null && PropertyFrame.CurrentSourcePageType != typeof(GeneralPropertyPage))
            {
                PropertyFrame.Navigate(typeof(GeneralPropertyPage), Model);
            }
            else if (page != null && PropertyFrame.CurrentSourcePageType != page || page != null && PropertyFrame.DataContext != item)
            {
                PropertyFrame.Navigate(page, item);
            }
        }

        /// <summary>
        /// Wechselt die Eigenschaftsseite
        /// </summary>
        /// <param name="page">Die Seite, zu der gewechselt werden soll</param>
        /// <param name="item">Das Item</param>
        public void ChangePage(Type page, Item item)
        {
            Titel.DataContext = item;

            if (page == null && PropertyFrame.CurrentSourcePageType != typeof(GeneralPage))
            {
                ItemFrame.Navigate(typeof(GeneralPage), Model);
            }
            else if (page != null && PropertyFrame.CurrentSourcePageType != page || page != null && PropertyFrame.DataContext != item)
            {
                ItemFrame.Navigate(page, item);
            }
        }

        /// <summary>
        /// Tritt ein, wenn ein Item aus dem Baum aufgerufen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItem as Item;
            
            ViewHelper.ChangePage(item);
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich der Zustand der Schaltfächen zum Ansicht ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnEdit(object sender, RoutedEventArgs e)
        {
            Tree.Visibility = Edit.IsChecked.Value ? Visibility.Collapsed : Visibility.Visible;
            PropertyFrame.Visibility = Edit.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung beendet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnExit(object sender, RoutedEventArgs e) => CoreApplication.Exit();

        /// <summary>
        /// Öffnet den Dialog zum Laden eines Dokumentes
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnOpenAsync(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            openPicker.FileTypeFilter.Add(".gx");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                Model.LoadProject(file);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Dokument gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSave(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Model.ProjectFileName))
            {
                OnSaveAsAsync(sender, e);

                return;
            }

            //Model.SaveProject(Model.ProjectFileName);
        }

        /// <summary>
        /// Wird aufgerufen, wenn gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private async void OnSaveAsAsync(object sender, RoutedEventArgs e)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = Path.GetFileNameWithoutExtension(string.IsNullOrEmpty(Model.ProjectFileName) ? "Neues Spiel" : Model.ProjectFileName)
            };

            savePicker.FileTypeChoices.Add("GameExpress", new List<string>() { ".gx" });

            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Model.SaveProject(file);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein Item gelöscht werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnDelete(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Szene erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddScene(object sender, RoutedEventArgs e)
        {
            var select = Tree.SelectedNodes.FirstOrDefault();

            //if (select != null)
            //{

            //}
            //else
            {
                var game = Model.Tree.FirstOrDefault();
                game.Children.Add(new ItemScene() { Name = "Neue Szene" });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neues Objekt erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddObject(object sender, RoutedEventArgs e)
        {
            var select = Tree.SelectedNodes.FirstOrDefault();

            //if (select != null)
            //{

            //}
            //else
            {
                var game = Model.Tree.FirstOrDefault();
                game.Children.Add(new ItemObject() { Name = "Neues Objekt" });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Karte erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddMap(object sender, RoutedEventArgs e)
        {
            var select = Tree.SelectedNodes.FirstOrDefault();

            //if (select != null)
            //{

            //}
            //else
            {
                var game = Model.Tree.FirstOrDefault();
                game.Children.Add(new ItemMap() { Name = "Neue Karte" });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neues Bild erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddImage(object sender, RoutedEventArgs e)
        {
            var select = Tree.SelectedNodes.FirstOrDefault();

            //if (select != null)
            //{

            //}
            //else
            {
                var game = Model.Tree.FirstOrDefault();
                game.Children.Add(new ItemImage() { Name = "Neues Bild" });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn eine neuer Sound erstellt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddSound(object sender, RoutedEventArgs e)
        {
            var select = Tree.SelectedNodes.FirstOrDefault();

            //if (select != null)
            //{

            //}
            //else
            {
                var game = Model.Tree.FirstOrDefault();
                game.Children.Add(new ItemSound() { Name = "Neuer Sound" });
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn auf der Titelleiste geklickt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnTitelPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = (ItemFrame.Content as Page)?.DataContext as Item;
            ViewHelper.ChangePropertyPage(item);
        }
    }
}
