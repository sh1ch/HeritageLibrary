using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Heritage.Wpf.Control;

public class FontIcon : TabControl
{
	public static readonly DependencyProperty GlyphProperty = 
		DependencyProperty.Register(
			nameof(Glyph), 
			typeof(char), 
			typeof(FontIcon), 
			new PropertyMetadata('\uE7E8'));

	public char Glyph
	{
		get => (char)GetValue(GlyphProperty);
		set => SetValue(GlyphProperty, value);
	}

	static FontIcon()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIcon), new FrameworkPropertyMetadata(typeof(FontIcon)));
	}
}
