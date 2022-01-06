using Heritage.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Heritage.Test;

public partial class DataFactory
{
    public static List<TestData<int>[]> TSourceInt = new List<TestData<int>[]>
    {
        new TestData<int>[]
        {
            new (2, 3),
            new (3, 6),
            new (8, 8),
            new (9, 1),
            new (9, 1),
        },
    };

    public static List<TestData<int?>[]> TSourceNullableInt = new List<TestData<int?>[]>
    {
        new TestData<int?>[]
        {
            new (2, null),
            new (null, null),
            new (-9, null),
            new (9, null),
            new (9, null),
        },
    };

    public static List<TestData<double>[]> TSourceDouble = new List<TestData<double>[]>
    {
        new TestData<double>[]
        {
            new (2.2, 3.3),
            new (3.3, 6.6),
            new (8.8, 8.8),
            new (9.9, 1.1),
            new (9.9, 1.1),
        },
    };

    public static List<TestData<double?>[]> TSourceNullableDouble = new List<TestData<double?>[]>
    {
        new TestData<double?>[]
        {
            new (2.2, null),
            new (null, null),
            new (-9.9, null),
            new (9.9, null),
            new (9.9, null),
        },
    };

    public static List<TestData<float>[]> TSourceFloat = new List<TestData<float>[]>
    {
        new TestData<float>[]
        {
            new (2.2F, 3.3F),
            new (3.3F, 6.6F),
            new (8.8F, 8.8F),
            new (9.9F, 1.1F),
            new (9.9F, 1.1F),
        },
    };

    public static List<TestData<float?>[]> TSourceNullableFloat = new List<TestData<float?>[]>
    {
        new TestData<float?>[]
        {
            new (2.2F, null),
            new (null, null),
            new (-9.9F, null),
            new (9.9F, null),
            new (9.9F, null),
        },
    };

    public static List<TestData<decimal>[]> TSourceDecimal = new List<TestData<decimal>[]>
    {
        new TestData<decimal>[]
        {
            new (2.2M, 3.3M),
            new (3.3M, 6.6M),
            new (8.8M, 8.8M),
            new (9.9M, 1.1M),
            new (9.9M, 1.1M),
        },
    };

    public static List<TestData<decimal?>[]> TSourceNullableDecimal = new List<TestData<decimal?>[]>
    {
        new TestData<decimal?>[]
        {
            new (2.2M, null),
            new (null, null),
            new (-9.9M, null),
            new (9.9M, null),
            new (9.9M, null),
        },
    };
}

public partial class LinqTests
{
    [TestCaseSource(typeof(DataFactory), "TSourceInt")]
    public void int_�ő�l_�ŏ��l(TestData<int>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), 8);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), 2);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), 1);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceNullableInt")]
    public void nullable_int_�ő�l_�ŏ��l(TestData<int?>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), null);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), -9);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), null);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceDouble")]
    public void double_�ő�l_�ŏ��l(TestData<double>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), 8.8);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), 2.2);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), 1.1);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceNullableDouble")]
    public void nullable_double_�ő�l_�ŏ��l(TestData<double?>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), null);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), -9.9);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), null);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceFloat")]
    public void float_�ő�l_�ŏ��l(TestData<float>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9F);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), 8.8F);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), 2.2F);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), 1.1F);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceNullableFloat")]
    public void nullable_float_�ő�l_�ŏ��l(TestData<float?>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9F);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), null);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), -9.9F);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), null);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceDecimal")]
    public void decimal_�ő�l_�ŏ��l(TestData<decimal>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9F);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), 8.8F);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), 2.2F);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), 1.1F);
    }

    [TestCaseSource(typeof(DataFactory), "TSourceNullableDecimal")]
    public void nullable_decimal_�ő�l_�ŏ��l(TestData<decimal?>[] args)
    {
        Assert.AreEqual(args.MaxOrDefault(p => p.Data1), 9.9F);
        Assert.AreEqual(args.MaxOrDefault(p => p.Data2), null);

        Assert.AreEqual(args.MinOrDefault(p => p.Data1), -9.9F);
        Assert.AreEqual(args.MinOrDefault(p => p.Data2), null);
    }
}