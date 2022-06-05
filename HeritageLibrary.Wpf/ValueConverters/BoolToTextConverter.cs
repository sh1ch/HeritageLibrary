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
/// <see cref="BoolToTextConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="bool"/> 型のデータを、指定したテキストに変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(bool), typeof(string))]
public class BoolToTextConverter : DependencyObject, IValueConverter
{
    #region DependencyProperties

    public static readonly DependencyProperty NullTextProperty =
            DependencyProperty.Register(
                nameof(NullText),
                typeof(string),
                typeof(BoolToTextConverter),
                new PropertyMetadata("")
            );

    /// <summary>
    /// <c>null</c> 値の代替テキストを取得または設定します。
    /// </summary>
    public string NullText
    {
        get => (string)GetValue(NullTextProperty);
        set => SetValue(NullTextProperty, value);
    }

    public static readonly DependencyProperty TrueProperty =
            DependencyProperty.Register(
                nameof(TrueText),
                typeof(string),
                typeof(BoolToTextConverter),
                new PropertyMetadata("〇")
            );

    /// <summary>
    /// <c>true</c> 値の代替テキストを取得または設定します。
    /// </summary>
    public string TrueText
    {
        get => (string)GetValue(TrueProperty);
        set => SetValue(TrueProperty, value);
    }

    public static readonly DependencyProperty FalseProperty =
            DependencyProperty.Register(
                nameof(FalseText),
                typeof(string),
                typeof(BoolToTextConverter),
                new PropertyMetadata("〇")
            );

    /// <summary>
    /// <c>false</c> 値の代替テキストを取得または設定します。
    /// </summary>
    public string FalseText
    {
        get => (string)GetValue(FalseProperty);
        set => SetValue(FalseProperty, value);
    }

    #endregion


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return NullText;
        }

        string text = "";

        if (value is bool)
        {
            var canParse = bool.TryParse(value.ToString(), out bool result);

            if (canParse)
            {
                text = result ? TrueText : FalseText;
            }
            else
            {
                text = NullText;
            }
        }

        return text;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
