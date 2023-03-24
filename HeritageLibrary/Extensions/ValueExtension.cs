using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Extensions;

/// <summary>
/// <see cref="ValueExtension"/> クラスは、<see cref="string"/> 型の表示値に変換するための拡張です。
/// </summary>
public static class ValueExtension
{
    /// <summary>
    /// <see cref="bool"/> 型のデータを ON/OFF を表すテキストに変換します。
    /// </summary>
    /// <param name="value">変換する値。</param>
    /// <returns>ON または OFF を表すテキスト。</returns>
    public static string ToTextONorOFF(this bool value)
    {
        return value ? "ON" : "OFF";
    }

    /// <summary>
    /// 指定した <see cref="double"/> 型の値を最も近い桁数の値に丸めた値を取得します。
    /// </summary>
    /// <param name="value">丸め対象の数値。</param>
    /// <param name="decimals">小数部の桁数。</param>
    /// <param name="rounding">丸目方法を指定する列挙型。（デフォルトは四捨五入）</param>
    /// <returns>丸められた小数部の桁数を持つ数値。</returns>
    public static double ToRound(this double value, int decimals, MidpointRounding rounding = MidpointRounding.AwayFromZero)
    {
        return Math.Round(value, decimals, rounding);
    }

    /// <summary>
    /// 指定した <see cref="double"/> 型の値を最も近い桁数の値に丸めたテキストを取得します。
    /// </summary>
    /// <param name="value">丸め対象の数値。</param>
    /// <param name="decimals">小数部の桁数。</param>
    /// <param name="format">テキストに変換するときのフォーマット。</param>
    /// <param name="rounding">丸目方法を指定する列挙型。（デフォルトは四捨五入）</param>
    /// <returns>丸められた小数部の桁数を持つ数値を表すテキスト。</returns>
    public static string ToRoundText(this double value, int decimals, string format, MidpointRounding rounding = MidpointRounding.AwayFromZero)
    {
        return ToRound(value, decimals, rounding).ToString(format);
    }

    /// <summary>
    /// 指定した <see cref="double"/> 型の値を最も近い桁数の値に丸めたテキストを取得します。
    /// </summary>
    /// <param name="value">丸め対象の数値。</param>
    /// <param name="decimals">小数部の桁数。</param>
    /// <param name="format">テキストに変換するときのフォーマット。</param>
    /// <param name="rounding">丸目方法を指定する列挙型。（デフォルトは四捨五入）</param>
    /// <returns>丸められた小数部の桁数を持つ数値を表すテキスト。</returns>
    public static string ToRoundTextF(this double value, int decimals, MidpointRounding rounding = MidpointRounding.AwayFromZero)
    {
        return ToRoundText(value, decimals, $"F{decimals}", rounding);
    }
}
