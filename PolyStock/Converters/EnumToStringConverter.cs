using System;
using System.Globalization;
using Xamarin.Forms;

namespace PolyStock.Converters
{
    public abstract class BaseConverter<T, T2> : IValueConverter
    {
        protected abstract T FromT2(T2 t2);

        protected abstract T2 FromT(T  t2);


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FromT2((T2)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FromT((T)value);
        }
    }
}