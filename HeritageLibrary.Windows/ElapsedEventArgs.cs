using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Windows;

/// <summary>
/// <see cref="ElapsedEventArgs"/> クラスは、イベント データを格納するクラスです。<see cref="MultimediaTimer.Elapsed"/> イベントのデータを提供します。
/// </summary>
public class ElapsedEventArgs
{
	/// <summary>
	/// <see cref="MultimediaTimer.Elapsed"/> イベントが発生したシステム時刻 (ms) を取得します。
	/// </summary>
	public uint StartedTime { get; init; }

	/// <summary>
	/// <see cref="MultimediaTimer.Elapsed"/> イベントが発生した日時/時刻を取得します。
	/// </summary>
	public DateTime SignalTime { get; init; }
}
