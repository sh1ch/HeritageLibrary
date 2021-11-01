using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Heritage.Wpf.Extensions
{
    /// <summary>
    /// <see cref="Heritage.Wpf"/> に関連する拡張メソッドを定義します。
    /// </summary>
    public static class UtilityExtension
    {
        /// <summary>
        /// <see cref="Dispatcher"/> が関連付けられているスレッドで、<see cref="Action"/> を非同期的に実行します。
        /// </summary>
        /// <param name="action">引数を受け取らないメソッドへの <see cref="Action"/>。この <see cref="Action"/> は、<see cref="Dispatcher"/> イベント キューにプッシュされます。</param>
        /// <returns><see cref="BeginInvoke(Dispatcher, Action)"/> が呼び出された直後に返される <see cref="DispatcherOperation"/> オブジェクト。イベントキューで実行が保留されているアクション（実際はデリゲート）とやり取りするために使用できます。</returns>
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
        {
            return dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// <see cref="Dispatcher"/>が関連付けられているスレッドで、<see cref="Action"/> を指定された優先度で非同期に実行します。
        /// </summary>
        /// <param name="priority"><see cref="Dispatcher"/> イベントキューにある他の保留中のオペレーションと比較して、指定されたメソッドが呼び出される際の優先順位です。</param>
        /// <param name="action">引数を受け取らないメソッドへの <see cref="Action"/>。この <see cref="Action"/> は、<see cref="Dispatcher"/> イベント キューにプッシュされます。</param>
        /// <returns><see cref="BeginInvoke(Dispatcher, DispatcherPriority, Action)"/> が呼び出された直後に返される <see cref="DispatcherOperation"/> オブジェクト。イベントキューで実行が保留されているアクション（実際はデリゲート）とやり取りするために使用できます。</returns>
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, DispatcherPriority priority, Action action)
        {
            return dispatcher.BeginInvoke(priority, action);
        }
    }
}
