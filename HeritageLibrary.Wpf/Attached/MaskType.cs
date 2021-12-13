using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Wpf.Attached;

/// <summary>
/// <see cref="MaskType"/> 列挙型は、<see cref="Mask"/> の設定方法を示す識別子を表します。
/// </summary>
public enum MaskType : int
{
    /// <summary>
    /// 未設定
    /// </summary>
    None = 0x00,
    /// <summary>
    /// 半角数字 (0-9)
    /// </summary>
    HalfNumeric = 0x01,
    /// <summary>
    /// 半角英数字 (a-zA-Z0-9)
    /// </summary>
    HalfAlphanumeric = 0x02,
    /// <summary>
    /// 全角かな
    /// </summary>
    HIRAGANA = 0x03,
    /// <summary>
    /// 全角カナ
    /// </summary>
    KATAKANA = 0x04,
}
