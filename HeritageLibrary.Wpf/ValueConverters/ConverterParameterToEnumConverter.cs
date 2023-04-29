using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="ConverterParameterToEnumConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// コンバーターパラメーターのテキスト値を <seealso cref="Enum"/> 型のデータに変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(object), typeof(string))]
public class ConverterParameterToEnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return null; 
        }

        return value.ToString() == parameter.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return Binding.DoNothing;
        }

        var canParse = bool.TryParse(value.ToString(), out bool result);

        if (canParse)
        {
            if (result)
            {
                return Enum.Parse(targetType, parameter.ToString());
            }
        }

        return Binding.DoNothing;
    }
}