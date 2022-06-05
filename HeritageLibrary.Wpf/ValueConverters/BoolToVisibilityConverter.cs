using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="BoolToVisibilityConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="bool"/> 型のデータを <seealso cref="System.Windows.Visibility"/> 型のデータに変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(bool?), typeof(Visibility))]
public class BoolToVisibilityConverter : DependencyObject, IValueConverter
{
    #region DependencyProperties

    public static readonly DependencyProperty TrueValueProperty =
        DependencyProperty.Register(
            nameof(TrueValue),
            typeof(Visibility),
            typeof(BoolToVisibilityConverter),
            new PropertyMetadata(Visibility.Visible)
        );

    /// <summary>
    /// <c>true</c> のときに返却する <see cref="Visibility"/> 型の値を取得または設定します。
    /// </summary>
    public Visibility TrueValue
    {
        get => (Visibility)GetValue(TrueValueProperty);
        set => SetValue(TrueValueProperty, value);
    }

    public static readonly DependencyProperty FalseValueProperty =
        DependencyProperty.Register(
            nameof(FalseValue),
            typeof(Visibility),
            typeof(BoolToVisibilityConverter),
            new PropertyMetadata(Visibility.Collapsed)
        );

    /// <summary>
    /// <c>false</c> のときに返却する <see cref="Visibility"/> 型の値を取得または設定します。
    /// </summary>
    public Visibility FalseValue
    {
        get => (Visibility)GetValue(FalseValueProperty);
        set => SetValue(FalseValueProperty, value);
    }

    public static readonly DependencyProperty NullValueProperty =
        DependencyProperty.Register(
            nameof(NullValue),
            typeof(Visibility),
            typeof(BoolToVisibilityConverter),
            new PropertyMetadata(Visibility.Collapsed)
        );

    /// <summary>
    /// <c>null</c> のときに返却する <see cref="Visibility"/> 型の値を取得または設定します。
    /// </summary>
    public Visibility NullValue
    {
        get =>(Visibility)GetValue(NullValueProperty);
        set => SetValue(NullValueProperty, value);
    }

    #endregion

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return NullValue;
        }

        var canParse = bool.TryParse(value.ToString(), out bool result);

        if (canParse)
        {
            return result ? TrueValue : FalseValue;
        }
        
        return NullValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        var canParse = Enum.TryParse(value.ToString(), out Visibility result);

        if (canParse)
        {
            if (result == TrueValue) return true;
            if (result == FalseValue) return false;
        }

        return null;
    }
}
