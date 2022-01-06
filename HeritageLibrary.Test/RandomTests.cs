using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Test;

public class RandomTests
{
    [SetUp]
    public void Setup()
    {

    }

    [TestCase(1, 151, 1000)]
    [TestCase(-100, 200, 1000)]
    [TestCase(-999999, 999999, 10000)]
    public void int_state_範囲テスト(int min, int max, int count)
    {
        var random = new Heritage.Mathematics.RandomState(100);
        var data = new List<int>();

        for (int i = 0; i < count; i++)
        {
            data.Add(random.Range(min, max));
        }

        Assert.IsTrue(WithinRange(data, min, max));
    }

    [TestCase(1.5, 151.5, 1000)]
    [TestCase(-100.1, 200.2, 1000)]
    [TestCase(-999999.9, 999999.9, 10000)]
    public void double_state_範囲テスト(double min, double max, int count)
    {
        var random = new Heritage.Mathematics.RandomState(100);
        var data = new List<double>();

        for (int i = 0; i < count; i++)
        {
            data.Add(random.Range(min, max));
        }

        Assert.IsTrue(WithinRange(data, min, max));
    }

    [TestCase(1, 151, 1000)]
    [TestCase(-100, 200, 1000)]
    [TestCase(-999999, 999999, 10000)]
    public void int_範囲テスト(int min, int max, int count)
    {
        Heritage.Mathematics.Random.SetSeed(500);
        var data = new List<int>();

        for (int i = 0; i < count; i++)
        {
            data.Add(Heritage.Mathematics.Random.Range(min, max));
        }

        Assert.IsTrue(WithinRange(data, min, max));
    }

    [TestCase(1.5, 151.5, 1000)]
    [TestCase(-100.1, 200.2, 1000)]
    [TestCase(-999999.9, 999999.9, 10000)]
    public void double_範囲テスト(double min, double max, int count)
    {
        Heritage.Mathematics.Random.SetSeed(500);
        var data = new List<double>();

        for (int i = 0; i < count; i++)
        {
            data.Add(Heritage.Mathematics.Random.Range(min, max));
        }

        Assert.IsTrue(WithinRange(data, min, max));
    }

    private bool WithinRange(IEnumerable<int> values, int min, int max) => !values.Any(v => v < min || v > max);
    private bool WithinRange(IEnumerable<double> values, double min, double max) => !values.Any(v => v < min || v > max);

}
