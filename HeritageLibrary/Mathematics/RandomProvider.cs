using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heritage.Mathematics;

/// <summary>
/// <see cref="RandomProvider"/> クラスは、スレッドセーフな乱数を提供します。
/// </summary>
public class RandomProvider
{
    private static int _Seed = Environment.TickCount;

    private static ThreadLocal<RandomState> _RandomWrapper = 
        new ThreadLocal<RandomState>(() =>  new RandomState(Interlocked.Increment(ref _Seed)));

    /// <summary>
    /// 疑似乱数ジェネレーターの状態を決めるシード値を設定します。
    /// </summary>
    /// <param name="seed">擬似乱数系列の開始値を計算するために使用するシード値。</param>
    public static void SetSeed(long seed)
    {
        var state = GetThreadRandom();

        state.SetSeed((ulong)seed);
    }

    /// <summary>
    /// 疑似乱数ジェネレーターの状態を決めるシード値を設定します。
    /// </summary>
    /// <param name="seed">擬似乱数系列の開始値を計算するために使用するシード値。</param>
    public static void SetSeed(ulong seed)
    {
        var state = GetThreadRandom();

        state.SetSeed(seed);
    }

    /// <summary>
    /// 指定回数だけ擬似乱数の計算を実行します。
    /// <para>
    /// 乱数の推測を回避したり、初期化の目的に利用します。
    /// </para>
    /// </summary>
    /// <param name="count">実行回数。</param>
    public static void Stretch(int count)
    {
        var state = GetThreadRandom();

        for (int i = 0; i < count; i++)
        {
            _ = state.GetNext();
        }
    }

    /// <summary>
    /// 指定した範囲のランダムな数値を取得します。（指定した値を含む）
    /// </summary>
    /// <param name="minValue">最小値。</param>
    /// <param name="maxValue">最大値。</param>
    /// <returns>疑似乱数値。</returns>
    public static int Range(int minValue, int maxValue)
    {
        var state = GetThreadRandom();

        return state.Range(minValue, maxValue);
    }

    /// <summary>
    /// 指定した範囲のランダムな数値を取得します。（指定した値を含む）
    /// </summary>
    /// <param name="minValue">最小値。</param>
    /// <param name="maxValue">最大値。</param>
    /// <returns>疑似乱数値。</returns>
    public static double Range(double minValue = 0.0D, double maxValue = 1.0D)
    {
        var state = GetThreadRandom();

        return state.Range(minValue, maxValue);
    }

    /// <summary>
    /// 疑似乱数を取得します。
    /// <para>
    /// 乱数の範囲は、0 ≦ w ≦ (2^64) -1 です。（値を含む）
    /// </para>
    /// </summary>
    /// <returns>疑似乱数値。</returns>
    public static ulong GetNext()
    {
        var state = GetThreadRandom();

        return state.GetNext();
    }

    private static RandomState GetThreadRandom() => _RandomWrapper.Value!;
}
