using Heritage.Extensions;
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

    [TestCase("abcd",  3, new string[] { "a", "b", "c" }, new string[] { "d", "d", "d" }, "dddd")]
    [TestCase("aaad",  1, new string[] { "a" }, new string[] { "d" }, "dddd")]
    [TestCase("dddd",  1, new string[] { "a" }, new string[] { "d" }, "dddd")]
    [TestCase("dddd\r\n",  2, new string[] { "\r", "\n" }, new string[] { @"\r", @"\n" }, @"dddd\r\n")]
    public void Test_Replaces(string source, int count, string[] oldValues, string[] newValues, string answer)
    {
        var tuples = new List<(string, string)>();

        for (int i = 0; i < count; i++)
        {
            tuples.Add((oldValues[i], newValues[i]));
        }

        var textValue = source.Replaces(tuples.ToArray());

        Assert.AreEqual(textValue, answer);
    }

    [TestCase(true, "ON")]
    [TestCase(false, "OFF")]
    public void Test_ValueExtension_ToTextONorOFF(bool source, string answer)
    {
        var result = source.ToTextONorOFF();

        Assert.AreEqual(result, answer);
    }

    [TestCase(0.125D, 2, 0.13D)]
    [TestCase(0.123456D, 3, 0.123D)]
    public void Test_ValueExtension_ToToRound(double source, int digits, double answer)
    {
        var result = source.ToRound(digits);

        Assert.AreEqual(result, answer);
    }

    [TestCase(0.125D, 2, "0.13")]
    [TestCase(0.123456D, 3, "0.123")]
    public void Test_ValueExtension_ToToRoundTextF(double source, int digits, string answer)
    {
        var result = source.ToRoundTextF(digits);

        Assert.AreEqual(result, answer);
    }
}