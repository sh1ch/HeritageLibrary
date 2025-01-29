using Heritage.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Heritage.Wpf.ValueConverters;

/// <summary>
/// <see cref="ResourcesDescriptionConverter"/> クラスは、カスタム ロジックをバインディングに適用する方法を提供します。 
/// <para>
/// <seealso cref="object"/> 型のデータが持つ属性値を変換値とします。
/// </para>
/// </summary>
[ValueConversion(typeof(object), typeof(string))]
public class ResourcesDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return "";
        }

        if (value is Array array)
        {
			var attributeValues = new List<string>();

			foreach (var item in array)
            {
				var fieldInfo = item.GetType().GetField(item.ToString());
				
				if (fieldInfo != null)
                {
					var attribute = fieldInfo.GetCustomAttribute(typeof(ResourcesDescriptionAttribute), false) as ResourcesDescriptionAttribute;

					if (attribute != null)
					{
						attributeValues.Add(attribute.Description);
					}
					else
					{
						attributeValues.Add(item.ToString());
					}
				}
			}

			return attributeValues;
        }
        else
        {
			var attributeValue = "";
			var fieldInfo = value.GetType().GetField(value.ToString());

			if (fieldInfo != null)
			{
				var attribute = fieldInfo.GetCustomAttribute(typeof(ResourcesDescriptionAttribute), false) as ResourcesDescriptionAttribute;
				attributeValue = attribute?.Description ?? "";
			}
			else
			{
				Debug.WriteLine($"{nameof(ResourcesDescriptionConverter)}: fieldInfo の取得に失敗しました。Enum 値のテキストの置換はできません。");
			}

			return attributeValue;
		}
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
}
