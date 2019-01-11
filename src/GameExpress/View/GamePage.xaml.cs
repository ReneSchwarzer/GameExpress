using GameExpress.Model.Item;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Ansichtseite des Spiels
    /// </summary>
    public sealed partial class GamePage : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public GamePage()
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
            Editor.Item = e.Parameter as ItemGame;

            ViewHelper.ChangePropertyPage(e.Parameter as Item);
        }
    }
}
