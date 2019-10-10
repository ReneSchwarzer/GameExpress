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
    /// Ansichtsseite einer Karte
    /// </summary>
    public sealed partial class MapPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Karte
        /// </summary>
        private ItemMap Map => DataContext as ItemMap;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MapPage()
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
            Editor.Item = e.Parameter as ItemMap;

            ViewHelper.ChangePropertyPage(e.Parameter as Item);

            Editor.SelectedItems.Add(Map);
            Editor.FitSize();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Control geladen wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            Editor.MergeCommandBar(ToolBar, false);
            ToolBar.Visibility = Visibility.Collapsed;
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
        /// Wird aufgerufen, wenn kein atives Item mehr ausgewählt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSelectedItemLost(object sender, System.EventArgs e)
        {
            Editor.SelectedItems.Add(Map);
        }

        /// <summary>
        /// Wird aufgerufen, ein Handle (Vertext) ausgewählt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSelectHandleChange(object sender, SelectionFrames.ISelectionFrameHandle e)
        {
            ViewHelper.ChangePropertyPage(e?.Item);
        }

        /// <summary>
        /// Wird aufgerufen, wenn ein neuer Vertext hinzugefügt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddVertext(object sender, RoutedEventArgs e)
        {
            Map.Vertices.Add(new ItemMapVertext() { Vector = new Model.Structs.Vector() });

            Editor.Invalidate();
        }
    }
}