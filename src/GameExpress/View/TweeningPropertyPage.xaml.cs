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
    /// Eigenschaftsseite des Tweeningobjektes
    /// </summary>
    public sealed partial class TweeningPropertyPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Tweeningobjekt
        /// </summary>
        private ItemKeyFrameTweening KeyFrame { get { return DataContext as ItemKeyFrameTweening; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TweeningPropertyPage()
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
    }
}
