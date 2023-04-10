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
/// <see cref="NullableBoolToBoolConverter"/> クラスは、 クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="bool?"/> 型のデータを、<seealso cref="bool"/> に変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(bool?), typeof(bool))]
public class NullableBoolToBoolConverter : DependencyObject, IValueConverter
{
    public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(
                nameof(NullValue),
                typeof(bool),
                typeof(NullableBoolToBoolConverter),
                new PropertyMetadata(false)
            );

    public bool NullValue
    {
        get => (bool)GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return NullValue;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
}
