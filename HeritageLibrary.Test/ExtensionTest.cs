using Heritage.Linq;
using Heritage.Systems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heritage.Test;

public class ExtensionTest
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(100, true, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99 })]
    [TestCase(100, false, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 100 })]
    [TestCase(100, false, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 101 })]
    public void Test_GreaterThan_Int(int testValue, bool answer, int[] parameters)
    {
        var result = testValue.GreaterThan(parameters);

        Assert.AreEqual(result, answer);
    }

    [TestCase(100, true, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99 })]
    [TestCase(100, true, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 100 })]
    [TestCase(100, false, new int[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 101 })]
    public void Test_GreaterThanOrEqual_Int(int testValue, bool answer, int[] parameters)
    {
        var result = testValue.GreaterThanOrEqual(parameters);

        Assert.AreEqual(result, answer);
    }

    [TestCase(100, true, new int[] { 101, 120, 130, 140 })]
    [TestCase(100, false, new int[] { 100, 101, 120, 130, 140 })]
    [TestCase(100, false, new int[] { 0, 120, 130, 140 })]
    public void Test_LessThan_Int(int testValue, bool answer, int[] parameters)
    {
        var result = testValue.LessThan(parameters);

        Assert.AreEqual(result, answer);
    }

    [TestCase(100, true, new int[] { 101, 120, 130, 140 })]
    [TestCase(100, true, new int[] { 100, 101, 120, 130, 140 })]
    [TestCase(100, false, new int[] { 0, 120, 130, 140 })]
    public void Test_LessThanOrEqual_Int(int testValue, bool answer, int[] parameters)
    {
        var result = testValue.LessThanOrEqual(parameters);

        Assert.AreEqual(result, answer);
    }

    [TestCase(100D, true, new double[] { -10, 0, 10, 20, 30, 40, 60, 80, 99 })]
    [TestCase(100D, false, new double[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 100 })]
    [TestCase(100D, false, new double[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 101 })]
    public void Test_GreaterThan_Double(double testValue, bool answer, double[] parameters)
    {
        var result = testValue.GreaterThan(parameters);

        Assert.AreEqual(result, answer);
    }

    [TestCase(100F, true, new float[] { -10, 0, 10, 20, 30, 40, 60, 80, 99 })]
    [TestCase(100F, false, new float[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 100 })]
    [TestCase(100F, false, new float[] { -10, 0, 10, 20, 30, 40, 60, 80, 99, 101 })]
    public void Test_GreaterThan_Float(float testValue, bool answer, float[] parameters)
    {
        var result = testValue.GreaterThan(parameters);

        Assert.AreEqual(result, answer);
    }
}