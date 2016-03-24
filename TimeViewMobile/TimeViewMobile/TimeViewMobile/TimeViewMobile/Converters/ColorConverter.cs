using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimeViewMobile.Converters 
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value.ToString().ToLower())
            {

                case "aqua":
                    return Color.Aqua;
                case "black":
                    return Color.Black;
                case "blue":
                    return Color.Blue;
                case "fuchsia":
                    return Color.Fuchsia;
                case "gray":
                    return Color.Gray;
                case "green":
                    return Color.Green;
                case "lime":
                    return Color.Lime;
                case "maroon":
                    return Color.Maroon;
                case "navy":
                    return Color.Navy;
                case "olive":
                    return Color.Olive;
                case "pink":
                    return Color.Pink;
                case "purple":
                    return Color.Purple;
                case "red":
                    return Color.Red;
                case "silver":
                    return Color.Silver;
                case "teal":
                    return Color.Teal;
                case "white":
                    return Color.White;
                case "yellow":
                    return Color.Yellow;
                default:
                    return Color.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
