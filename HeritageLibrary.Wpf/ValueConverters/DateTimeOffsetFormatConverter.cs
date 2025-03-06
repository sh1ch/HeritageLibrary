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
/// <see cref="DateTimeOffsetFormatConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="DateTimeOffset"/> 型のデータを、言語にあわせた書式のテキストに変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(DateTimeOffset), typeof(string))]
public class DateTimeOffsetFormatConverter : DependencyObject, IValueConverter
{
	public static readonly DependencyProperty TextFormatProperty =
			DependencyProperty.Register(
				nameof(TextFormat),
				typeof(string),
				typeof(DateTimeOffsetFormatConverter),
				new PropertyMetadata("")
			);

	public string TextFormat
	{
		get => (string)GetValue(TextFormatProperty);
		set => SetValue(TextFormatProperty, value);
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTimeOffset selectedDateTime;
        string displayValue = "";
        
        if (value == null)
        {
            return ""; 
        }

        string formatParameter = parameter?.ToString() ?? "";
        string formatSpecifier = !string.IsNullOrEmpty(formatParameter) ? $":{formatParameter?.ToString()}" : "";

        if (string.IsNullOrEmpty(formatSpecifier))
        {
            formatSpecifier = TextFormat;
        }

        if (value is DateTimeOffset? || value is DateTimeOffset)
        {
            selectedDateTime = (DateTimeOffset)value;

            if (selectedDateTime == DateTimeOffset.MinValue || selectedDateTime == DateTimeOffset.MaxValue)
	        {
                // 例外
				displayValue = "";
			}
            else if (!string.IsNullOrEmpty(formatSpecifier))
            {
                // フォーマットの指定あり
                displayValue = string.Format("{0:" + formatSpecifier + "}", selectedDateTime);
            }
            else
            {
                // フォーマットの指定なし
                displayValue = selectedDateTime.ToString();
            }
        }

        return displayValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
