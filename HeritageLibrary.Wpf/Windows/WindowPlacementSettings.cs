using Heritage.Wpf.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using static Heritage.Wpf.Native.ApiHelper;

namespace Heritage.Wpf.Windows;

/// <summary>
/// <see cref="WindowPlacementSettings"/> クラスは、ウィンドウの状態・位置情報を設定します。
/// </summary>
public class WindowPlacementSettings
{
	public static WINDOWPLACEMENT? Read(System.Windows.Window window)
	{
		var hwnd = new WindowInteropHelper(window).Handle;
		return Read(hwnd);
	}

	public static WINDOWPLACEMENT? Read(IntPtr hwnd)
	{
		WINDOWPLACEMENT placement;
		ApiHelper.GetWindowPlacement(hwnd, out placement);

		return placement;
	}

	public static void Write(System.Windows.Window window, WINDOWPLACEMENT placement)
	{
		var hwnd = new WindowInteropHelper(window).Handle;
		Write(hwnd, placement);
	}

	public static void Write(IntPtr hwnd, WINDOWPLACEMENT placement)
	{
		placement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
		placement.flags = 0;
		placement.showCmd = (placement.showCmd == SW.SHOWMINIMIZED) ? SW.SHOWNORMAL : placement.showCmd;

		ApiHelper.SetWindowPlacement(hwnd, ref placement);
	}
}
