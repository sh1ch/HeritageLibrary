using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Heritage.Wpf.Controls;

public class BlindBase : UserControl
{
	public static readonly DependencyProperty IsActiveProperty =
		DependencyProperty.Register
		(
			nameof(IsActive),
			typeof(bool),
			typeof(BlindBase),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
			(s, e) =>
			{
				if (s is BlindBase blind)
				{
					blind.OnIsActiveChanged(blind, e);
				}
			})
		);

	public bool IsActive
	{
		get => (bool)GetValue(IsActiveProperty);
		set => SetValue(IsActiveProperty, value);
	}

	/// <summary>
	/// <see cref="BlindBase"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public BlindBase()
    {
		if (DesignerProperties.GetIsInDesignMode(this))
		{
			return;
		}

		Hide();
    }

	private void OnIsActiveChanged(BlindBase _, DependencyPropertyChangedEventArgs e)
	{
		var newValue = (bool)e.NewValue;
		var oldValue = (bool)e.OldValue;

		if (newValue != oldValue)
		{
			if (newValue == true)
			{
				Show();
			}
			else
			{
				Hide();
			}
		}
	}

	public void Show()
	{
		Visibility = Visibility.Visible;
		IsActive = true;
	}

	public void Hide()
	{
		Visibility = Visibility.Hidden;
		IsActive = false;
	}
}
