using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage;

/// <summary>
/// <see cref="Extension"/> クラスは、拡張メソッドを定義します。
/// </summary>
public static class Extension
{
    /// <summary>
    /// Create a <see cref="System.Collections.Generic.IEnumerable{T}"/> from a <see cref="System.Collections.IList"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">The <see cref="System.Collections.IList"/> to create a <see cref="System.Collections.Generic.IEnumerable{T}"/>.</param>
    /// <returns>A <see cref="System.Collections.Generic.IEnumerable{T}"/> that contains elements from the input sequence.</returns>
    public static List<T> ToList<T>(this System.Collections.IList source)
    {
        return source?.Cast<T>().ToList() ?? new List<T>();
    }

    /// <summary>
    /// Create an array from a <see cref="System.Collections.IList"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">The <see cref="System.Collections.IList"/> to create an array.</param>
    /// <returns>An array that contains elements from the input sequence.</returns>
    public static T[] ToArray<T>(this System.Collections.IList source)
    {
        return source?.Cast<T>().ToArray() ?? Array.Empty<T>();
    }

    /// <summary>
    /// オブジェクトは、引数のいずれかの値と等しいかどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>等しい値を含んでいたとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
    public static bool IsAny<T>(this T own, params T[] items) where T : IComparable
    {
        bool isEquals = false;

        foreach (var item in items)
        {
            if (own.Equals(item))
            {
                isEquals = true;
                break;
            }
        }

        return isEquals;
    }

    /// <summary>
    /// オブジェクトは、引数のいずれよりも大きい値かどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>条件を満たすとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
    public static bool GreaterThan<T>(this T own, params T[] items) where T : IComparable
    {
        bool isGreater = true;

        foreach (var item in items)
        {
            if (own.CompareTo(item) <= 0)
            {
                isGreater = false;
                break;
            }
        }

        return isGreater;
    }

    /// <summary>
    /// オブジェクトは、引数のいずれよりも大きい値、または、同じ値かどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>条件を満たすとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
    public static bool GreaterThanOrEqual<T>(this T own, params T[] items) where T : IComparable
    {
        bool isGreater = true;

        foreach (var item in items)
        {
            if (own.CompareTo(item) < 0)
            {
                isGreater = false;
                break;
            }
        }

        return isGreater;
    }

    /// <summary>
    /// オブジェクトは、引数のいずれよりも小さい値かどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>条件を満たすとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
    public static bool LessThan<T>(this T own, params T[] items) where T : IComparable
    {
        bool isLess = true;

        foreach (var item in items)
        {
            if (own.CompareTo(item) >= 0)
            {
                isLess = false;
                break;
            }
        }

        return isLess;
    }

    /// <summary>
    /// オブジェクトは、引数のいずれよりも小さい値、または、同じ値かどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>条件を満たすとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
    public static bool LessThanOrEqual<T>(this T own, params T[] items) where T : IComparable
    {
        bool isLess = true;

        foreach (var item in items)
        {
            if (own.CompareTo(item) > 0)
            {
                isLess = false;
                break;
            }
        }

        return isLess;
    }

    /// <summary>
    /// バイト配列のデータを 16 進数のテキストに変換します。
    /// </summary>
    /// <param name="bytes">バイト配列のデータ。</param>
    /// <param name="distance">バイトデータとバイトデータの間に含める区間テキスト。</param>
    /// <returns>16 進数のテキストデータ。データが存在しないとき空白文字 <c>""</c> を返却します。</returns>
    /// <exception cref="ArgumentNullException">バイト配列が <c>null</c> のとき発生する例外。</exception>
    public static string ToHex(this byte[] bytes, string distance = "-")
    {
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));
        if (bytes.Length <= 0) return "";

        var hex = BitConverter.ToString(bytes);
        return hex.Replace("-", distance);
    }
}
