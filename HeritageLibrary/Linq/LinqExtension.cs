using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Linq;

/// <summary>
/// <see cref="LinqExtension"/> クラスは、<see cref="System.Linq"/> の拡張メソッドを定義します。
/// </summary>
public static class LinqExtension
{
    public static int MaxOrDefault(this IEnumerable<int> source) => source?.Count() > 0 ? source.Max() : default(int);
    public static int? MaxOrDefault(this IEnumerable<int?> source) => source?.Count() > 0 ? source.Max() : null;
    public static long MaxOrDefault(this IEnumerable<long> source) => source?.Count() > 0 ? source.Max() : default(long);
    public static long? MaxOrDefault(this IEnumerable<long?> source) => source?.Count() > 0 ? source.Max() : null;
    public static double MaxOrDefault(this IEnumerable<double> source) => source?.Count() > 0 ? source.Max() : default(double);
    public static double? MaxOrDefault(this IEnumerable<double?> source) => source?.Count() > 0 ? source.Max() : null;
    public static decimal MaxOrDefault(this IEnumerable<decimal> source) => source?.Count() > 0 ? source.Max() : default(decimal);
    public static decimal? MaxOrDefault(this IEnumerable<decimal?> source) => source?.Count() > 0 ? source.Max() : null;
    public static float MaxOrDefault(this IEnumerable<float> source) => source?.Count() > 0 ? source.Max() : default(float);
    public static float? MaxOrDefault(this IEnumerable<float?> source) => source?.Count() > 0 ? source.Max() : null;

    public static int MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) => source?.Count() > 0 ? source.Max(selector) : default(int);
    public static int? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) => source?.Count() > 0 ? source.Max(selector) : default(int?);
    public static double MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) => source?.Count() > 0 ? source.Max(selector) : default(double);
    public static double? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) => source?.Count() > 0 ? source.Max(selector) : default(double?);
    public static decimal MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) => source?.Count() > 0 ? source.Max(selector) : default(decimal);
    public static decimal? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) => source?.Count() > 0 ? source.Max(selector) : default(decimal?);
    public static float MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) => source?.Count() > 0 ? source.Max(selector) : default(float);
    public static float? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) => source?.Count() > 0 ? source.Max(selector) : default(float?);

    public static int MinOrDefault(this IEnumerable<int> source) => source?.Count() > 0 ? source.Min() : default(int);
    public static int? MinOrDefault(this IEnumerable<int?> source) => source?.Count() > 0 ? source.Min() : null;
    public static long MinOrDefault(this IEnumerable<long> source) => source?.Count() > 0 ? source.Min() : default(long);
    public static long? MinOrDefault(this IEnumerable<long?> source) => source?.Count() > 0 ? source.Min() : null;
    public static double MinOrDefault(this IEnumerable<double> source) => source?.Count() > 0 ? source.Min() : default(double);
    public static double? MinOrDefault(this IEnumerable<double?> source) => source?.Count() > 0 ? source.Min() : null;
    public static decimal MinOrDefault(this IEnumerable<decimal> source) => source?.Count() > 0 ? source.Min() : default(decimal);
    public static decimal? MinOrDefault(this IEnumerable<decimal?> source) => source?.Count() > 0 ? source.Min() : null;
    public static float MinOrDefault(this IEnumerable<float> source) => source?.Count() > 0 ? source.Min() : default(float);
    public static float? MinOrDefault(this IEnumerable<float?> source) => source?.Count() > 0 ? source.Min() : null;

    public static int MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) => source?.Count() > 0 ? source.Min(selector) : default(int);
    public static int? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector) => source?.Count() > 0 ? source.Min(selector) : default(int?);
    public static double MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) => source?.Count() > 0 ? source.Min(selector) : default(double);
    public static double? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector) => source?.Count() > 0 ? source.Min(selector) : default(double?);
    public static decimal MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector) => source?.Count() > 0 ? source.Min(selector) : default(decimal);
    public static decimal? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector) => source?.Count() > 0 ? source.Min(selector) : default(decimal?);
    public static float MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector) => source?.Count() > 0 ? source.Min(selector) : default(float);
    public static float? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector) => source?.Count() > 0 ? source.Min(selector) : default(float?);

}
