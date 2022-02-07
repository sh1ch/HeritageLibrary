using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="InverseBoolConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="bool?"/> 型のデータを反転した値に変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(bool?), typeof(bool?))]
public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (targetType != typeof(bool) && targetType != typeof(bool?))
        {
            return null;
        }

        return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (targetType != typeof(bool) && targetType != typeof(bool?))
        {
            return null;
        }

        return !(bool)value;
    }
}
