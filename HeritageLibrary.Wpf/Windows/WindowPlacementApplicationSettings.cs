using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heritage.Wpf.Native.ApiHelper;

namespace Heritage.Wpf.Windows;

public class WindowPlacementApplicationSettings : ApplicationSettingsBase
{
	[UserScopedSetting]
	public WINDOWPLACEMENT? Placement
	{
		get => this["Placement"] != null ? (WINDOWPLACEMENT?)(WINDOWPLACEMENT)this["Placement"] : null;
		set => this["Placement"] = value;
	}

	/// <summary>
	/// <see cref="WindowPlacementApplicationSettings"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public WindowPlacementApplicationSettings()
	{
	}
}
