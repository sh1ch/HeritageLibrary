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
    /// オブジェクトは、 引数のいずれかの値と等しいかどうかを判断します。
    /// </summary>
    /// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
    /// <param name="own">値を確認するオブジェクト。</param>
    /// <param name="items">含んでいるかどうかを確認する値。</param>
    /// <returns>等しい値を含んでいたとき true 。それ以外のとき false 。</returns>
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
}
