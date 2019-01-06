using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace GameExpress.Core.Converter
{
    public class GammaTypeConverter : TypeConverter
    {
        /**
         * Gibt zurück, ob dieser Konverter das Objekt mithilfe des angegebenen Kontexts in den angegebenen Typ konvertieren kann.
         * 
         * @param context Eine ITypeDescriptorContext-Schnittstelle, die einen Formatierungskontext bereitstellt. 
         * @param destinationType Eine Type-Klasse, die den Zieltyp der Konvertierung darstellt. 
         * @return true, wenn dieser Konverter die Konvertierung durchführen kann, andernfalls false.
         */
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(Structs.Gamma))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /**
         * Konvertiert das angegebene Wertobjekt in den angegebenen Typ.
         *
         * @param context Eine ITypeDescriptorContext-Schnittstelle, die einen Formatierungskontext bereitstellt. 
         * @param culture Eine CultureInfo-Klasse. Wenn nullNothingnullptrNULL-Verweis übergeben wird, wird von der aktuellen Kultur ausgegangen. 
         * @param value Die zu konvertierende Object-Klasse. 
         * @param destinationType Die Type-Klasse, in die der value-Parameter konvertiert werden soll.
         */
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is Structs.Gamma)
            {
                Structs.Gamma a = (Structs.Gamma)value;
                return a.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /**
         * Gibt zurück, ob dieser Konverter ein Objekt des angegebenen Typs mithilfe des angegebenen Kontexts in den Typ des Konverters konvertieren kann.
         *
         * @param context Eine ITypeDescriptorContext-Schnittstelle, die einen Formatierungskontext bereitstellt. 
         * @param sourceType Eine Type-Klasse, die den Ausgangstyp der Konvertierung darstellt. 
         * @return true, wenn dieser Konverter die Konvertierung durchführen kann, andernfalls false.
         */
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /**
         * Konvertiert das angegebene Objekt unter Verwendung des angegebenen Kontexts und der Kulturinformationen in den Typ dieses Konverters.
         * 
         * @param context Eine ITypeDescriptorContext-Schnittstelle, die einen Formatierungskontext bereitstellt. 
         * @param culture Die als aktuelle Kultur zu verwendende CultureInfo-Klasse. 
         * @param value Die zu konvertierende Object-Klasse. 
         * @return Eine Object-Klasse, die den konvertierten Wert darstellt.
         */
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    s = s.Split("%".ToCharArray())[0];
                    float i = float.Parse(s);
                    Structs.Gamma g = new Structs.Gamma(i);

                    return g;
                }
                catch
                {
                    throw new ArgumentException(" '" + (string)value + "' kann nicht in Typ Alpha konvertiert werden.");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
