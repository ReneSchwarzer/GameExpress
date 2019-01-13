using GameExpress.Model.Item;
using GameExpress.View;
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

namespace GameExpress.Dialog
{
    /// <summary>
    /// Instanz-Auswahldialog
    /// </summary>
    public sealed partial class SelectInstanceDialog : ContentDialog
    {
        /// <summary>
        /// Liefert oder setzt das aktuelle Item
        /// </summary>
        public ItemAnimation CurrentItem { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SelectInstanceDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            List.ItemsSource = ViewHelper.Project.Tree.FirstOrDefault().GetPreOrder().ToList();

            List.SelectedItem = SelectedItem;

            CheckBox.Visibility = CurrentItem == null ? Visibility.Collapsed : Visibility.Visible; 
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control entladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnUnloaded(object sender, RoutedEventArgs args)
        {

        }

        /// <summary>
        /// Tritt ein, wenn sich die Auswahl der Liste ändert
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = e.AddedItems.FirstOrDefault() as Item;

            IsPrimaryButtonEnabled = SelectedItem != null;
        }

        /// <summary>
        /// Tritt ein, wenn die globale Suche in eine lokale Suche ändert oder umgedreht
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (CheckBox.IsChecked == true || CurrentItem == null)
            {
                List.ItemsSource = ViewHelper.Project.Tree.FirstOrDefault().GetPreOrder().ToList();
            }
            else
            {
                List.ItemsSource = CurrentItem.GetPreOrder().ToList();
            }

            List.SelectedItem = SelectedItem;
        }

        /// <summary>
        /// Liefert oder setzt das ausgewählte Item
        /// </summary>
        public Item SelectedItem
        {
            get { return (Item)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Item), typeof(SelectInstanceDialog), new PropertyMetadata(null));

    }
}
