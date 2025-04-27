using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace Heritage.Wpf.ValueConverters;

[ValueConversion(typeof(Visibility), typeof(bool?))]
public class VisibilityToBoolConverter : DependencyObject, IValueConverter
{
	public static readonly DependencyProperty IsInverseProperty =
		DependencyProperty.Register(
			nameof(IsInverse),
			typeof(bool),
			typeof(VisibilityToBoolConverter),
			new PropertyMetadata(false)
		);

	public bool IsInverse
	{
		get => (bool)GetValue(IsInverseProperty);
		set => SetValue(IsInverseProperty, value);
	}

	public static readonly DependencyProperty TrueValueProperty =
		DependencyProperty.Register(
			nameof(TrueValue),
			typeof(Visibility),
			typeof(VisibilityToBoolConverter),
			new PropertyMetadata(Visibility.Visible)
		);

	public Visibility TrueValue
	{
		get => (Visibility)GetValue(TrueValueProperty);
		set => SetValue(TrueValueProperty, value);
	}

	public static readonly DependencyProperty FalseValueProperty =
		DependencyProperty.Register(
			nameof(FalseValue),
			typeof(Visibility),
			typeof(VisibilityToBoolConverter),
			new PropertyMetadata(Visibility.Collapsed)
		);

	public Visibility FalseValue
	{
		get => (Visibility)GetValue(FalseValueProperty);
		set => SetValue(FalseValueProperty, value);
	}

	public static readonly DependencyProperty NullValueProperty =
		DependencyProperty.Register(
			nameof(NullValue),
			typeof(bool?),
			typeof(VisibilityToBoolConverter),
			new PropertyMetadata(false)
		);

	public bool? NullValue
	{
		get => (bool?)GetValue(NullValueProperty);
		set => SetValue(NullValueProperty, value);
	}


	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return NullValue;
		}

		if (value is Visibility visibility)
		{
			bool result = visibility == TrueValue;

			if (IsInverse)
			{
				result = !result;
			}

			return result;
		}

		return NullValue;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}

		if (value is bool b)
		{
			if (IsInverse)
			{
				b = !b;
			}

			return b ? TrueValue : FalseValue;
		}

		return null;
	}
}
