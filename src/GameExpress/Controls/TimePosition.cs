using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace GameExpress.Controls
{
    public sealed class TimePosition : Control
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public TimePosition()
        {
            this.DefaultStyleKey = typeof(TimePosition);
        }

        /// <summary>
        /// Wird beim Anwenden des Templates (EditorPanel.xaml-Datei) aufgerufen
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        /// <summary>
        /// Liefert oder setzt die Animationszeit
        /// </summary>
        public ulong Time
        {
            get { return (ulong)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Time.
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(ulong), typeof(TimePosition), new PropertyMetadata(new ulong()));

    }
}
