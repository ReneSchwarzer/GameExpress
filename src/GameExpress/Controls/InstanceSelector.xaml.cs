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

namespace GameExpress.Controls
{
    public sealed partial class InstanceSelector : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public InstanceSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoaded(object sender, RoutedEventArgs args)
        {
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
        /// Wird aufgerufen, wenn die zugehörige Instanz geändert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private async void OnChangeInstance(object sender, RoutedEventArgs args)
        {
            var element = args.OriginalSource as Button;
            if (element == null)
            {
                return;
            }

            var dialog = new SelectInstanceDialog()
            {
                //CurrentItem = Item?.Instance,
                SelectedItem = Item?.Instance
            };

            switch (await dialog.ShowAsync())
            {
                case ContentDialogResult.Primary:
                    {
                        Item.ID = dialog.SelectedItem?.ID;
                        Item.Name = dialog.SelectedItem?.Name;
                    }
                    break;
            }

        }

        /// <summary>
        /// Liefert oder setzt das Item
        /// </summary>
        public ItemInstance Item
        {
            get => (ItemInstance)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Item. This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register
        (
            "Item",
            typeof(ItemInstance),
            typeof(InstanceSelector),
            new PropertyMetadata(null)
        );

    }
}
