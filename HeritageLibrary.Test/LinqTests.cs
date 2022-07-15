using Heritage.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Heritage.Test;

public record TestData<T>
{
    public T Data1;
    public T Data2;

    public TestData(T data1, T data2)
    {
        Data1 = data1;
        Data2 = data2;
    }
}

public partial class DataFactory
{
    public static List<int?[]> NullableInt = new List<int?[]>
    {
        new int?[] { 1, 2, 3, 0, -9, 8, 9, 2, null }
    };

    public static List<long?[]> NullableLong = new List<long?[]>
    {
        new long?[] { 1, 2, 3, 0, -9, 8, 9, 2, null }
    };

    public static List<double?[]> NullableDouble = new List<double?[]>
    {
        new double?[] { 1.1, 2.2, 3.3, 0.0, -9.9, 8.8, 9.9, 2.2, null }
    };

    public static List<float?[]> NullableFloat = new List<float?[]>
    {
        new float?[] { 1.1F, 2.2F, 3.3F, 0.0F, -9.9F, 8.8F, 9.9F, 2.2F, null }
    };

    public static List<decimal[]> Decimal = new List<decimal[]>
    {
        new decimal[] { 1.1M, 2.2M, 3.3M, 0.0M, -9.9M, 8.8M, 9.9M, 2.2M }
    };

    public static List<decimal?[]> NullableDecimal = new List<decimal?[]>
    {
        new decimal?[] { 1.1M, 2.2M, 3.3M, 0.0M, -9.9M, 8.8M, 9.9M, 2.2M, null }
    };
}

public partial class LinqTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(new int[] { })]
    public void int_default(int[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(int));
        Assert.AreEqual(args.MinOrDefault(), default(int));
    }

    [TestCase(null)]
    public void nullable_int_default(int?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(int?));
        Assert.AreEqual(args.MinOrDefault(), default(int?));
    }

    [TestCase(new long[] { })]
    public void long_default(long[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(long));
        Assert.AreEqual(args.MinOrDefault(), default(long));
    }

    [TestCase(null)]
    public void nullable_long_default(long?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(long?));
        Assert.AreEqual(args.MinOrDefault(), default(long?));
    }

    [TestCase(new double[] { })]
    public void double_default(double[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(double));
        Assert.AreEqual(args.MinOrDefault(), default(double));
    }

    [TestCase(null)]
    public void nullable_double_default(double?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(double?));
        Assert.AreEqual(args.MinOrDefault(), default(double?));
    }

    [TestCase(new float[] { })]
    public void float_default(float[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(float));
        Assert.AreEqual(args.MinOrDefault(), default(float));
    }

    [TestCase(null)]
    public void nullable_float_default(float?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(float?));
        Assert.AreEqual(args.MinOrDefault(), default(float?));
    }

    [TestCase(null)]
    public void decimal_default(decimal[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(decimal));
        Assert.AreEqual(args.MinOrDefault(), default(decimal));
    }

    [TestCase(null)]
    public void nullable_decimal_default(decimal?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), default(decimal?));
        Assert.AreEqual(args.MinOrDefault(), default(decimal?));
    }

    [TestCase(new int[] { 1, 2, 3, 9, 8, 2 })]
    public void int_最大値_最小値(int[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9);
        Assert.AreEqual(args.MinOrDefault(), 1);
    }

    [TestCase(new long[] { 1, 2, 3, 9, 8, 2 })]
    public void long_最大値_最小値(long[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9);
        Assert.AreEqual(args.MinOrDefault(), 1);
    }

    [TestCase(new double[] { 1.1, 2.2, 3.3, 9.8, 8.2, 2.5 })]
    public void double_最大値_最小値(double[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.8);
        Assert.AreEqual(args.MinOrDefault(), 1.1);
    }

    [TestCase(new float[] { 1.1F, 2.2F, 3.3F, 9.8F, 8.2F, 2.5F })]
    public void float_最大値_最小値(float[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.8F);
        Assert.AreEqual(args.MinOrDefault(), 1.1F);
    }

    [TestCaseSource(typeof(DataFactory), "Decimal")]
    public void decimal_最大値_最小値(decimal[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.9D);
        Assert.AreEqual(args.MinOrDefault(), -9.9D);
    }


    [TestCaseSource(typeof(DataFactory), "NullableInt")]
    public void nullable_int_最大値_最小値(int?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9);
        Assert.AreEqual(args.MinOrDefault(), -9);
    }

    [TestCaseSource(typeof(DataFactory), "NullableLong")]
    public void nullable_long_最大値_最小値(long?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9);
        Assert.AreEqual(args.MinOrDefault(), -9);
    }

    [TestCaseSource(typeof(DataFactory), "NullableDouble")]
    public void nullable_double_最大値_最小値(double?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.9);
        Assert.AreEqual(args.MinOrDefault(), -9.9);
    }

    [TestCaseSource(typeof(DataFactory), "NullableFloat")]
    public void nullable_float_最大値_最小値(float?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.9F);
        Assert.AreEqual(args.MinOrDefault(), -9.9F);
    }

    [TestCaseSource(typeof(DataFactory), "NullableDecimal")]
    public void nullable_decimal_最大値_最小値(decimal?[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(), 9.9D);
        Assert.AreEqual(args.MinOrDefault(), -9.9D);
    }
}