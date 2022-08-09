using Heritage.ExpressionParser;
using Heritage.Linq;
using Heritage.Systems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heritage.Test;

public class ExpressionParserTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Text_Add()
    {
        var parser = new TextToNode();
        var lexicalAnalyzer = new LexicalAnalysis();
        var expression = new Expression();
        double? resultValue;

        try
        {
            var node = parser.Parse("1+2+3");

            // 字句解析
            lexicalAnalyzer.Perform(node);

            resultValue = expression.Execute(node);
        }
        catch
        {
            resultValue = null;
        }

        Assert.AreEqual(resultValue, 6);
    }

    [TestCase("x", 10, 16)]
    [TestCase("y", null, null)]
    public void Text_AttributedAdd(string name, double? value, double? actual)
    {
        var parser = new TextToNode();
        var lexicalAnalyzer = new LexicalAnalysis();
        var expression = new Expression();
        double? resultValue;

        try
        {
            var node = parser.Parse($"1+2+3+{name}");

            // 字句解析
            lexicalAnalyzer.Perform(node);

            // 変数値の存在をチェックする
            var attributeNames = node.GetAttributes().OrderBy(p => p);

            // 変数の代入
            var attribute = new AttributeValue(name, value);

            expression.AddAttribute(attribute);
            resultValue = expression.Execute(node);
        }
        catch
        {
            resultValue = null;
        }

        Assert.AreEqual(resultValue, actual);
    }
}
