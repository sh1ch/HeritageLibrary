using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heritage.Windows;

/// <summary>
/// <see cref="MultimediaTimer"/> クラスは、Windows マルチメディア timeapi を利用したタイマーです。
/// <para>
/// ミリ秒が経過した後に発生する反復イベントを生成します。オプションとして、一度だけ実行するメソッド実行を生成することも可能です。
/// </para>
/// </summary>
[SupportedOSPlatform("windows10.0.19041.0")]
public sealed class MultimediaTimer : IDisposable
{
	private static bool _NeedToInitDevice = true;
	private static TIMECAPS? _TIMECAPS = null;

	// ExecutionEngineException が発生するため、コールバックが呼び出されている間は delegate をインスタンスで保持
	private LPTIMECALLBACK? _TimeCallback = null;
	private List<OneShotCallback> OneShotCallbacks = new List<OneShotCallback>();
	private uint _TimerID = 0;

	private struct OneShotCallback
	{
		public LPTIMECALLBACK? Callback { get; init; }
		public uint TimerID { get; init; }
	}

	#region P/Invoke Dll

	[StructLayout(LayoutKind.Sequential)]
	public struct TIMECAPS
	{
		public UInt32 wPeriodMin;
		public UInt32 wPeriodMax;
	};

	private const int TIMERR_NOERROR = 0x00;
	private const int TIMERR_BASE = 0x60;
	private const int TIMERR_NOCANDO = TIMERR_BASE + 0x01;
	private const int TIMERR_STRUCT = TIMERR_BASE + 0x21;
	private const int TIME_ONESHOT = 0x00; // イベントを１度だけ実行
	private const int TIME_PERIODIC = 0x01; // イベントを繰り返し実行

	private delegate void LPTIMECALLBACK(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2);

	[DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeGetDevCaps")]
	static extern uint __TimeGetDevCaps(ref TIMECAPS timeCaps, uint sizeTimeCaps);

	[DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeSetEvent")]
	private static extern uint __TimeSetEvent(uint uDelay, uint uResolution, LPTIMECALLBACK lpTimeProc, UIntPtr dwUser, uint fuEvent);

	[DllImport("winmm.dll", SetLastError = true, EntryPoint = "timeKillEvent")]
	private static extern uint __TimeKillEvent(uint uTimerId);

	/// <summary>
	/// システム時刻をミリ秒単位で取得
	/// </summary>
	/// <returns>システム時刻をミリ秒単位</returns>
	[DllImport("winmm.dll", EntryPoint = "timeGetTime")]
	private static extern uint __GetTime();

	/// <param name="uMilliseconds">アプリケーションまたはデバイス ドライバーの最小タイマー解像度 (ms)</param>
	/// <returns>TIMERR_NOERROR を返却</returns>
	[DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
	private static extern uint __BeginPeriod(uint uMilliseconds);

	/// <summary>
	/// 以前に設定した最小タイマー解像度をクリア
	/// </summary>
	/// <param name="uMilliseconds">前回の呼び出しで指定された最小タイマーのクリア</param>
	/// <returns>TIMERR_NOERROR を返却</returns>
	[DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
	private static extern uint __EndPeriod(uint uMilliseconds);

	#endregion

	/// <summary>
	/// タイマーが実行中であるかどうかを示す値を取得します。
	/// </summary>
	public bool IsRunning => _TimerID != 0;

	/// <summary>
	/// タイマー解決でサポートできる待機時間 (ms) の精度（最小解像度）を表す値を取得します。
	/// </summary>
	public uint Resolution => _TIMECAPS?.wPeriodMin ?? 1;

	private uint _Interval = 0;

	/// <summary>
	/// <see cref="Elapsed"/> イベントの発生間隔 (ms) を取得または設定します。
	/// </summary>
	public uint Interval
	{
		get => _Interval;
		set
		{
			if (Interval != value)
			{
				if (value < (_TIMECAPS?.wPeriodMin ?? 1))
				{
					throw new ArgumentOutOfRangeException($"Invalid to set value ({value} ms) < resolution ({_TIMECAPS?.wPeriodMin ?? 1} ms). accuracy cannot be guaranteed.");
				}

				_Interval = value;
			}
		}
	}

	/// <summary>
	/// <see cref="Interval"/> (ms) が経過したときに発生するイベントです。
	/// </summary>
	public event EventHandler? Elapsed;

	/// <summary>
	/// <see cref="MultimediaTimer"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public MultimediaTimer() : this(1000)
	{
	}

	/// <summary>
	/// <see cref="MultimediaTimer"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="interval"><see cref="Elapsed"/> イベントの発生間隔 (ms) 。</param>
	public MultimediaTimer(uint interval)
	{
		InitializeDeviceTimeCaps();
		Interval = interval;
	}

	/// <summary>
	/// インスタンスが使用しているすべてのリソースを解放します。
	/// </summary>
	public void Dispose()
	{
		if (IsRunning)
		{
			Stop();
		}

		// delegate を解放
		_TimeCallback = null;
	}

	/// <summary>
	/// <see cref="Interval"/> (ms) が経過したあと、値を返さないカプセル化されたメソッドを実行します。
	/// </summary>
	/// <param name="action">値を返さないメソッド。</param>
	public void OneShot(Action action) => OneShot(action, Interval, _TIMECAPS?.wPeriodMin ?? 1);

	/// <summary>
	/// 指定した待機時間 <paramref name="interval"/> (ms) が経過したあと、値を返さないカプセル化されたメソッドを実行します。
	/// </summary>
	/// <param name="action">値を返さないメソッド。</param>
	/// <param name="interval">待機するミリ秒。</param>
	public void OneShot(Action action, uint interval)
	{
		Interval = interval;
		OneShot(action, interval, _TIMECAPS?.wPeriodMin ?? 1);
	}

	/// <exception cref="InvalidOperationException"></exception>
	private void OneShot(Action action, uint interval, uint resolution)
	{
		if (Interval <= resolution)
		{
			throw new InvalidOperationException($"Invalid to start timer. Interval ({interval} ms) is less than resolution ({resolution} ms).");
		}

		LPTIMECALLBACK? callback = (uTimerID, uMsg, dwUser, dw1, dw2) =>
		{
			action();
			DisposeOneshot(uTimerID);
		};

		var timerID = __TimeSetEvent(interval, resolution, callback, UIntPtr.Zero, TIME_ONESHOT);
		var newshot = new OneShotCallback { TimerID = timerID, Callback = callback };

		OneShotCallbacks.Add(newshot);
	}

	/// <summary>
	/// <see cref="IsRunning"/> を <c>true</c> にして、<see cref="Elapsed"/> イベントの発生を開始します。
	/// </summary>
	public void Start() => Start(Interval);

	/// <summary>
	/// <see cref="IsRunning"/> を <c>true</c> にして、<see cref="Elapsed"/> イベントの発生を開始します。
	/// </summary>
	/// <param name="interval"><see cref="Elapsed"/> イベントの発生する間隔 (ms)。</param>
	public void Start(uint interval)
	{
		Interval = interval;
		Start(interval, _TIMECAPS?.wPeriodMin ?? 1);
	}

	private void Start(uint interval, uint resolution)
	{
		if (IsRunning)
		{ 
			throw new InvalidOperationException("Timer is already running.");
		}

		if (Interval <= resolution)
		{
			throw new InvalidOperationException($"Invalid to start timer. Interval ({interval} ms) is less than resolution ({resolution} ms).");
		}

		_TimeCallback = (uTimerID, uMsg, dwUser, dw1, dw2) =>
		{
			var now = __GetTime();

			Elapsed?.Invoke(this, EventArgs.Empty);
		};

		_TimerID = __TimeSetEvent(interval, resolution, _TimeCallback, UIntPtr.Zero, TIME_PERIODIC);

		if (_TimerID == 0)
		{
			throw new InvalidOperationException("Failed to start multimedia timer.");
		}
	}

	/// <summary>
	/// <see cref="IsRunning"/> を <c>false</c> にして、<see cref="Elapsed"/> イベントの発生を停止します。
	/// </summary>
	public void Stop()
	{
		// 実行中でなければ、何もしない
		if (!IsRunning) return;

		var mmsys = __TimeKillEvent(_TimerID);

		if (mmsys != TIMERR_NOERROR)
		{
			throw new InvalidOperationException("Failed to stop multimedia timer.");
		}

		_TimerID = 0;

		// ※ delegate はここで null にしない。Dispose で安全に解放する
	}

	/// <summary>
	/// システム時刻をミリ秒単位で取得します。
	/// </summary>
	/// <returns>システム時刻をミリ秒単位で返却。</returns>
	public uint GetTime() => __GetTime();

	private static void InitializeDeviceTimeCaps()
	{
		if (!_NeedToInitDevice) return;

		var caps = new TIMECAPS();
		var mmsys = __TimeGetDevCaps(ref caps, (uint)Marshal.SizeOf(caps));

		if (mmsys != TIMERR_NOERROR)
		{
			throw new InvalidOperationException("Failed to get timer capabilities.");
		}

		_TIMECAPS = caps;
		__BeginPeriod(_TIMECAPS?.wPeriodMin ?? 1);

		AppDomain.CurrentDomain.ProcessExit += (_, _) => __EndPeriod(_TIMECAPS?.wPeriodMin ?? 1);

		_NeedToInitDevice = false;
	}

	private void DisposeOneshot(uint timerID)
	{
		if (OneShotCallbacks.Count(p => p.TimerID == timerID) != 1) return;

		var selectedShot = OneShotCallbacks.Single(p => p.TimerID == timerID);

		OneShotCallbacks.Remove(selectedShot);
	}
}
