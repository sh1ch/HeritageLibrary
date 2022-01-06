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
/// <see cref="PercentConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="decimal?"/> 型のデータを、パーセントの数値に変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(decimal?), typeof(string))]
/// </summary>
public class PercentConverter : DependencyObject, IValueConverter
{
    #region DependencyProperties

    public static readonly DependencyProperty NullTextProperty =
            DependencyProperty.Register(
                nameof(NullText),
                typeof(string),
                typeof(PercentConverter),
                new PropertyMetadata("")
            );

    /// <summary>
    /// null 値の代替テキストを取得または設定します。
    /// </summary>
    public string NullText
    {
        get => (string)GetValue(NullTextProperty);
        set => SetValue(NullTextProperty, value);
    }

    public static readonly DependencyProperty DecimalsProperty =
            DependencyProperty.Register(
                nameof(Decimals),
                typeof(int),
                typeof(PercentConverter),
                new PropertyMetadata(2)
            );

    /// <summary>
    /// 小数の桁数を取得または設定します。
    /// </summary>
    public int Decimals
    {
        get => (int)GetValue(DecimalsProperty);
        set => SetValue(DecimalsProperty, value);
    }

    public static readonly DependencyProperty MidpointRoundingProperty =
            DependencyProperty.Register(
                nameof(MidpointRounding),
                typeof(MidpointRounding),
                typeof(PercentConverter),
                new PropertyMetadata(MidpointRounding.AwayFromZero) // デフォルトは四捨五入
            );

    /// <summary>
    /// 数値丸めをする方法を取得または設定します。
    /// </summary>
    public MidpointRounding MidpointRounding
    {
        get => (MidpointRounding)GetValue(MidpointRoundingProperty);
        set => SetValue(MidpointRoundingProperty, value);
    }

    public static readonly DependencyProperty IsZeroAsNullProperty =
            DependencyProperty.Register(
                nameof(IsZeroAsNull),
                typeof(bool),
                typeof(PercentConverter),
                new PropertyMetadata(false)
            );

    /// <summary>
    /// 0 の値を null として扱うかどうかを示す値を取得または設定します。
    /// </summary>
    public bool IsZeroAsNull
    {
        get => (bool)GetValue(IsZeroAsNullProperty);
        set => SetValue(IsZeroAsNullProperty, value);
    }

    #endregion

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return NullText; 
        }

        string text;

        if (value is int or double)
        {
            double doubleValue = (double)value;

            if (double.IsNaN(doubleValue))
            {
                return "NaN";
            }

            if (double.IsInfinity(doubleValue))
            {
                return "∞";
            }

            if (IsZeroAsNull && doubleValue == 0)
            {
                return NullText;
            }

            text = $"{ToRoundText(doubleValue * 100, Decimals, MidpointRounding)} %";
        }
        else if (value is float floatValue)
        {
            if (float.IsNaN(floatValue))
            {
                return "NaN";
            }

            if (float.IsInfinity(floatValue))
            {
                return "∞";
            }

            if (IsZeroAsNull && floatValue == 0)
            {
                return NullText;
            }

            text = $"{ToRoundText(floatValue * 100, Decimals, MidpointRounding)} %";
        }
        else
        {
            text = "N/A";
        }

        return text;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }

    private double ToRound(double value, int decimals, MidpointRounding rounding) => System.Math.Round(value, decimals, rounding);

    private string ToRoundText(double value, int decimals, MidpointRounding rounding)
    {
        var d = new string('0', decimals);

        value = ToRound(value, decimals, rounding);

        return string.Format("{0:0." + d + "}", value);
    }
}
