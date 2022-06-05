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
}
