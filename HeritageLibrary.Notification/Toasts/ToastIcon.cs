using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Toasts;

/// <summary>
/// <see cref="ToastIcon"/> 列挙型は、トースト表示する際のアイコン種別を示します。
/// </summary>
public enum ToastIcon
{
    /// <summary>
    /// 情報。
    /// </summary>
    Info,
    /// <summary>
    /// 注意。
    /// </summary>
    Warning,
    /// <summary>
    /// エラー。
    /// </summary>
    Error,
}
