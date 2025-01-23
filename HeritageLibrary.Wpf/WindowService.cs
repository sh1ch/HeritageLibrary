using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heritage.Wpf;

/// <summary>
/// <see cref="WindowService"/> クラスは、<see cref="System.Windows.Window"/> に関するサービスを提供します。
/// </summary>
public class WindowService : IWindowService
{
	private readonly System.Windows.Window _window;

	public System.Windows.Window Window => _window;
	public IntPtr WindowHandle => new System.Windows.Interop.WindowInteropHelper(_window).Handle;

	/// <summary>
	/// <see cref="WindowService"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="window">ウィンドウのインスタンス。</param>
	public WindowService(System.Windows.Window window)
	{
		if (window == null)
		{
			throw new ArgumentNullException(nameof(window));
		}

		_window = window;

		// WindowHandle のアドレス値は、Window の非表示前だと 0x00 が返却される。
		// Window 表示後のボタン押下のタイミングなどで取得されることが望ましい
	}

	public void Close() => _window.Close();
}
