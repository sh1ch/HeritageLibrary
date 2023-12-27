using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.CompilerServices;

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

	public static readonly DependencyProperty GlyphBrushProperty =
		DependencyProperty.Register(
			nameof(GlyphBrush),
			typeof(System.Windows.Media.Brush),
			typeof(FontIcon),
			new PropertyMetadata(System.Windows.Media.Brushes.Black));

	public System.Windows.Media.Brush GlyphBrush
	{
		get => (System.Windows.Media.Brush)GetValue(GlyphBrushProperty);
		set => SetValue(GlyphBrushProperty, value);
	}

	public static readonly DependencyProperty GlyphSizeProperty =
		DependencyProperty.Register(
			nameof(GlyphSize),
			typeof(int),
			typeof(FontIcon),
			new PropertyMetadata(16));

	public int GlyphSize
	{
		get => (int)GetValue(GlyphSizeProperty);
		set => SetValue(GlyphSizeProperty, value);
	}

	static FontIcon()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIcon), new FrameworkPropertyMetadata(typeof(FontIcon)));
	}
}
