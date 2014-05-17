using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Game2048.ViewModels
{
    [ValueConversion(typeof(int), typeof(Color))]
    class GridForeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = (int)value;
            string colorText;
            switch (level)
            {
                case 1:
                    colorText = "#FF7C736A";
                    break;
                case 2:
                    colorText = "#FF7C736A";
                    break;
                default:
                    colorText = "#FFFFF7EB";
                    break;
            }
            return (Color)ColorConverter.ConvertFromString(colorText);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
