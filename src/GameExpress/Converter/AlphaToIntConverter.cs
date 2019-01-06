using GameExpress.Model.Structs;
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
    /// Konvertiert von Alpha zu Int und zurück
    /// </summary>
    public class AlphaToIntConverter : IValueConverter
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
            if (value is Alpha)
            {
                var buf = (byte)(Alpha) value;

                return (Int32) buf;
            }

            return 0;
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
            if (value is int)
            {
                var buf = (int) value;

                return new Alpha((byte) buf);
            }
            else if (value is double)
            {
                var buf = (double) value;

                return new Alpha((byte) buf);
            }

            return new Alpha();
        }
    }
}
