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
    class GridBackColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = (int) value;
            string colorText;
            switch (level)
            {
                case 1:  // 2
                    colorText = "#FFEEE4DA";
                    break;
                case 2:  // 4
                    colorText = "#FFECE0C8";
                    break;
                case 3: 
                    colorText = "#FFF2B179";
                    break;
                case 4:
                    colorText = "#FFF59563";
                    break;
                case 5:
                    colorText = "#FFF57C5F";
                    break;
                case 6:  //64
                    colorText = "#FFF65D3B";
                    break;
                case 7:  //128
                    colorText = "#FFEDCE71";
                    break;
                case 8:
                    colorText = "#FFEDCC61";
                    break;
                case 9:
                    colorText = "#FFECC850";
                    break;
                case 10:
                    colorText = "#FFEDC53F";
                    break;
                default:
                    colorText = "#FFEDC53F";
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
