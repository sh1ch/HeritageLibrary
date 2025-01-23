using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heritage.Wpf;

/// <summary>
/// <see cref="IWindowService"/> インターフェースは、<see cref="System.Windows.Window"/> に関するサービスを提供します。
/// </summary>
public interface IWindowService
{
	System.Windows.Window Window { get; }
	IntPtr WindowHandle { get; }

	void Close();
}
