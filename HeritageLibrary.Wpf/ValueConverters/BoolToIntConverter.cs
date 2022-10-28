using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="BoolToIntConverter"/> クラスは、 クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="bool"/> 型のデータを、<seealso cref="int"/> に変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(bool), typeof(int))]
public class BoolToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return DependencyProperty.UnsetValue;
        }

        if (value is not int)
        {
            return DependencyProperty.UnsetValue;
        }

        var isResult = false;
        var result1 = (int)value;
        var canParse = int.TryParse(parameter.ToString(), out int result2);

        if (canParse)
        {
            isResult = result1 == result2;
        }
        else
        {
            return DependencyProperty.UnsetValue;
        }

        return isResult;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter == null)
        {
            return DependencyProperty.UnsetValue;
        }

        return parameter;
    }
}
