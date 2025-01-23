using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Wpf.Windows;

public class PlacementWindow : System.Windows.Window
{
	/// <summary>
	/// <see cref="PlacementWindow"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public PlacementWindow()
	{
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);

		var settings = new WindowPlacementApplicationSettings();

		settings.Reload();

		if (settings.Placement.HasValue)
		{
			var placement = settings.Placement.Value;

			WindowPlacementSettings.Write(this, placement);
		}
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);

		if (e.Cancel)
		{
			return;
		}

		var placement = WindowPlacementSettings.Read(this);
		var settings = new WindowPlacementApplicationSettings();

		settings.Placement = placement;
		settings.Save();
	}
}
