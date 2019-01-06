using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Eigenschaftsseite einer Karte
    /// </summary>
    public sealed partial class MapPropertyPage : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MapPropertyPage()
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
