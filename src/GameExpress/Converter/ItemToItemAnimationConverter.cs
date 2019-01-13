using GameExpress.Controls;
using GameExpress.Model.Item;
using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GameExpress.Converter
{
    /// <summary>
    /// Konvertiert von ItemObject zu ItemInctance
    /// </summary>
    public class ItemToItemAnimationConverter : IValueConverter
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
            return value as ItemObject;
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
            return value as Item;
        }
    }
}
