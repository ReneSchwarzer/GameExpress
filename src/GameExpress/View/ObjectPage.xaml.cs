﻿using GameExpress.Model.Item;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExpress.View
{
    /// <summary>
    /// Ansichtsseite eines Objektes
    /// </summary>
    public sealed partial class ObjectPage : Page
    {
        /// <summary>
        /// Liefert das mit der Ansicht verbundene Bild
        /// </summary>
        private ItemObject Object { get { return DataContext as ItemObject; } }
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ObjectPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Zwingt das Control zum neuzeichnen
        /// </summary>
        public void Invalidate()
        {
            Editor.Invalidate();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            DataContext = args.Parameter as ItemObject;
            Editor.Item = args.Parameter as ItemObject;
            ViewHelper.ChangePropertyPage(args.Parameter as Item);

            Editor.Loaded += (s, e) =>
            {
                Editor.MergeCommandBar(ToolBar, false);
                ToolBar.Visibility = Visibility.Collapsed;
            };
        }
        
        /// <summary>
        /// Wird aufgerufen, wenn eine neue Story hinzugefügt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAddStory(object sender, RoutedEventArgs e)
        {
            Object.StoryBoard.Add(new ItemStory()
            {
                Name = "Story " + (Object.StoryBoard.Count + 1)
            });
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Previousbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPrevious(object sender, RoutedEventArgs e)
        {
            Play.IsChecked = false;
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
