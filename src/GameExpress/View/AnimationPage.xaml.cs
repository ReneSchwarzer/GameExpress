﻿using GameExpress.Model.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Ansichtseite einer Animation
    /// </summary>
    public sealed partial class AnimationPage : Page
    {
        /// <summary>
        /// Liefert die mit der Ansicht verbundene Animation
        /// </summary>
        private ItemAnimation Animation => DataContext as ItemAnimation;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AnimationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn zu dieser Seite gewechselt wird
        /// </summary>
        /// <param name="args">Das Eventargument</param>
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);

            DataContext = args.Parameter;
            Editor.Item = Animation;

            ViewHelper.ChangePropertyPage(args.Parameter as Item);

            Editor.FitSize();
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite geladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnLoading(Windows.UI.Xaml.FrameworkElement sender, object args)
        {
            Animation.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Seite entladen wurde
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs args)
        {
            Animation.PropertyChanged -= OnPropertyChanged;
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

        /// <summary>
        /// Wird aufgerufen, wenn eine neue Story hinzugefügt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnAddStory(object sender, RoutedEventArgs args)
        {
            Animation.StoryBoard.Add(new ItemStory()
            {
                Name = "Story " + (Animation.StoryBoard.Count + 1)
            });
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Previousbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPrevious(object sender, RoutedEventArgs args)
        {
            Play.IsChecked = false;
            Editor.Time = 0;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Playbutton gedrückt wird
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnPlay(object sender, RoutedEventArgs args)
        {
            var isChecked = Play.IsChecked.Value;
            var ticks = DateTime.Now.Ticks;

            Task.Run(async () =>
            {
                while (isChecked)
                {
                    var now = DateTime.Now.Ticks;
                    var delta = (ulong)(now - ticks) / 10000;

                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        Editor.Time = delta;
                        Editor.Invalidate();
                        isChecked = Play.IsChecked.Value;
                    });

                    Thread.Sleep(20);
                }
            });

        }
    }
}
