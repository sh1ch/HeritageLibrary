using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageLibrary.NLog;

/// <summary>
/// <see cref="DebugTraceListener"/> クラスは、NLog 用の出力に対応したトレース出力、または、デバッグ出力を監視する最もシンプルなリスナーです。
/// </summary>
public class DebugTraceListener : TraceListener
{
	private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

	public override void Write(string? message)
	{
		_Logger.Debug(message ?? "");
	}

	public override void WriteLine(string? message)
	{
		_Logger.Debug(message ?? "");
	}

	/// <summary>
	/// <see cref="DebugTraceListener"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public DebugTraceListener()
	{
	}
}
