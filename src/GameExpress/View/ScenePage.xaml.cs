using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    /// Ansichtsseite einer Szene
    /// </summary>
    public sealed partial class ScenePage : Page
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ScenePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            DataContext = args.Parameter as ItemScene;
            Editor.Item = args.Parameter as ItemScene;
            TimeLine.Instances = (args.Parameter as ItemScene).Instances;

            Editor.Loaded += (s, e) =>
            {
                Editor.MergeCommandBar(ToolBar, false);
                ToolBar.Visibility = Visibility.Collapsed;
            };
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Previousbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPrevious(object sender, RoutedEventArgs e)
        {
            Editor.Time = 0;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Playbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPlay(object sender, RoutedEventArgs e)
        {
            var locking = false;
            var isChecked = Play.IsChecked.Value;
            ulong time = Editor.Time;

            Task.Run(async () =>
            {
                while (isChecked)
                {
                    if (!locking)
                    {
                        locking = true;

                        time++;

                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            Editor.Time = time;
                            Editor.Invalidate();
                            isChecked = Play.IsChecked.Value;

                            locking = false;
                        });

                    }

                    Thread.Sleep(10);
                }
            });

        }
    }
}
