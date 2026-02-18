using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Timers;


public interface IIntervalTimer : IDisposable
{
	/// <summary>
	/// 実行間隔 (ms) を取得または設定します。
	/// </summary>
	uint Interval { get; set; }

	/// <summary>
	/// 実行中かどうかを示す値を取得します。
	/// </summary>
	bool IsRunning { get; }

	/// <summary>
	/// タイマーの周期で発生するイベントです。
	/// </summary>
	event EventHandler? Elapsed;

	void Start();
	void Stop();
}
