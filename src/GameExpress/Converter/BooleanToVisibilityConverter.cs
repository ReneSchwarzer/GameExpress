using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GameExpress.Converter
{
    /// <summary>
    /// Konvertiert von Visibility zu Boolean und zurück
    /// </summary>
    public class VisibilityToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Konvertieren
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility && (Visibility)value == Visibility.Visible)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Konvertiert zurück
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value == true)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
    }
}
