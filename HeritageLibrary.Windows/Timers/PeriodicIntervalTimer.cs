using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heritage.Timers;

public class PeriodicIntervalTimer : IIntervalTimer
{
	private readonly object _gate = new();

	private PeriodicTimer? _timer;
	private CancellationTokenSource? _cts;
	private Task? _loopTask;

	private uint _interval;
	private int _running;
	private bool _isDisposed;

	public uint Interval
	{
		get => Volatile.Read(ref _interval);
		set
		{
			if (value == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(value)); 
			}

			ThrowIfDisposed();

			// 実行中の変更は禁止
			if (IsRunning)
			{
				throw new InvalidOperationException("Cannot change Interval while running. Stop the timer first.");
			}

			Volatile.Write(ref _interval, value);
		}
	}

	public bool IsRunning => Volatile.Read(ref _running) != 0;

	public event EventHandler? Elapsed;

	/// <summary>
	/// <see cref="PeriodicTimer"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="interval">実行間隔 (ms)</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public PeriodicIntervalTimer(uint interval)
	{
		ArgumentOutOfRangeException.ThrowIfZero(interval);

		_interval = interval;
	}

	public void Start()
	{
		ThrowIfDisposed();

		lock (_gate)
	   	{
			if (IsRunning)
			{
				throw new InvalidOperationException("Timer is already running.");
			}

			var intervalMS = Volatile.Read(ref _interval);

			_cts = new CancellationTokenSource();
			_timer = new PeriodicTimer(TimeSpan.FromMilliseconds(intervalMS));

			// 先に running を立てる（Start直後の Stop 競合を安定させる）
			Volatile.Write(ref _running, 1);

			_loopTask = RunLoopAsync(_timer, _cts.Token);
		}
	}

	public void Stop()
	{
		if (_isDisposed) return;

		CancellationTokenSource? cts;
		PeriodicTimer? timer;
		Task? loop;

		lock (_gate)
		{
			if (!IsRunning) return;

			// 先に running を終了させる (Stop 直後の追加発火を抑止)
			Volatile.Write(ref _running, 0);

			cts = _cts;
			timer = _timer;
			loop = _loopTask;

			_cts = null;
			_timer = null;
			_loopTask = null;
		}

		// Cancel/Dispose は lock 外で（デッドロック/遅延を回避）
		try { cts?.Cancel(); } catch { /* best effort */ }
		try { timer?.Dispose(); } catch { /* best effort */ }
		try { cts?.Dispose(); } catch { /* best effort */ }

		// Stop は「止める要求」。完了同期したいなら StopAsync を追加する方が安全。
		_ = loop;
	}

	private async Task RunLoopAsync(PeriodicTimer timer, CancellationToken ct)
	{
		try
		{
			// WaitForNextTickAsync はキャンセルで例外 (OperationCanceledException) が発生する恐れ
			while (await timer.WaitForNextTickAsync(ct).ConfigureAwait(false))
			{
				// Stop 直後の残り tick を弾く（競合対策）
				if (Volatile.Read(ref _running) == 0)
				{
					break;
				}

				var handler = Elapsed;

				if (handler is null)
				{
					continue; 
				}

				try
				{
					handler.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"PeriodicTimer Elapsed handler exception. ex = {ex}");
				}
			}
		}
		catch (OperationCanceledException)
		{
			// Stop() によるキャンセル
			Debug.WriteLine("PeriodicTimer loop canceled.");
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"PeriodicTimer loop exception. ex = {ex}");
		}
	}

	public void Dispose()
	{
		if (_isDisposed)
		{
			return;
		}

		_isDisposed = true;

		Stop();
		GC.SuppressFinalize(this);
	}

	private void ThrowIfDisposed()
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException(nameof(PeriodicTimer)); 
		}
	}
}
