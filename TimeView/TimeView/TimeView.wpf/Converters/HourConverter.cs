using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TimeView.data;

namespace TimeView.wpf.Converters
{
    public class CategoryEntryHourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) {
                return "N/A";
            }

            CategoryEntry categoryEntry = (CategoryEntry) value;
            return categoryEntry.Start.ToString("hh:mm tt") + " - " + categoryEntry.End.ToString("hh:mm tt");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
