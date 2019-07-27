using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Ansichtseite des Bildes
    /// </summary>
    public sealed partial class ImagePage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Bild
        /// </summary>
        private ItemImage Image => DataContext as ItemImage;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ImagePage()
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

            ViewHelper.ChangePropertyPage(e.Parameter as Item);

            Editor.Item = Image;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite geladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoading(Windows.UI.Xaml.FrameworkElement sender, object args)
        {
            Image.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite entladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Image.PropertyChanged -= OnPropertyChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Eigenschaften des Bildes geändert haben
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Editor.Invalidate();
        }
    }
}
