using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Extensions;

/// <summary>
/// <see cref="ComparableExtension"/> クラスは、さまざまな型のデータを比較するときのサポートをする拡張です。
/// </summary>
public static class ComparableExtension
{
	/// <summary>
	/// オブジェクトは、引数のいずれかの値と等しいかどうかを判断します。
	/// </summary>
	/// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
	/// <param name="own">値を確認するオブジェクト。</param>
	/// <param name="items">含んでいるかどうかを確認する値。</param>
	/// <returns>等しい値を含んでいたとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
	public static bool IsAny(this int own, params int[] items)
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
	/// オブジェクトは、引数のいずれかの値と等しいかどうかを判断します。
	/// </summary>
	/// <typeparam name="T">確認をするオブジェクトの型。</typeparam>
	/// <param name="own">値を確認するオブジェクト。</param>
	/// <param name="items">含んでいるかどうかを確認する値。</param>
	/// <returns>等しい値を含んでいたとき <c>true</c>、それ以外のとき <c>false</c>。</returns>
	public static bool IsAny(this double own, params int[] items)
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
}
