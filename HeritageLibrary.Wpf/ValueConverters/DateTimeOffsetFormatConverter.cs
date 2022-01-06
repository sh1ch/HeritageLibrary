using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="DateTimeOffsetFormatConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="DateTimeOffset"/> 型のデータを、言語にあわせた書式のテキストに変換します。
/// </para>
/// </summary>
[ValueConversion(typeof(DateTimeOffset), typeof(string))]
public class DateTimeOffsetFormatConverter : IValueConverter
{
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

        if (value is DateTimeOffset? || value is DateTimeOffset)
        {
            selectedDateTime = (DateTimeOffset)value;

            if (!string.IsNullOrEmpty(formatSpecifier))
            {
                displayValue = string.Format("{0" + formatSpecifier + "}", selectedDateTime);
            }
            else
            {
                if (selectedDateTime == DateTimeOffset.MinValue || selectedDateTime == DateTimeOffset.MaxValue)
                {
                    displayValue = "";
                }
                else
                {
                    displayValue = selectedDateTime.ToString();
                }
            }
        }

        return displayValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
