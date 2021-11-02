using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HeritageLibrary.Wpf.Controls.ValueConverters
{
    internal class BoolToScaleXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFlipHorizontal = (bool)value;

            return isFlipHorizontal ? -1 : 1; // -1 = 水平反転, 1 = 水平反転無し
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
