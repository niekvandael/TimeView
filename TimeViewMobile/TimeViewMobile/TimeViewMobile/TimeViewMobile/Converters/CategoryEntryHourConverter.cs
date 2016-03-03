using System;
using System.Globalization;
using TimeView.data;
using Xamarin.Forms;

namespace TimeViewMobile.Converters
{
    public class CategoryEntryHourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "N/A";
            }

            var categoryEntry = (CategoryEntry) value;
            return categoryEntry.Start.ToString("hh:mm tt") + " - " + categoryEntry.End.ToString("hh:mm tt");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}