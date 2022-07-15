using Heritage.Tasks;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Heritage.Test;

public class RetryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Action_例外数()
    {
        int attempts = Retry.Attempts;

        try
        {
            await Retry.InvokeAsync(TestAction);
        }
        catch (AggregateException e)
        {
            Assert.AreEqual(e.Flatten().InnerExceptions.Where(p => p is InvalidOperationException).Count(), attempts);
        }
    }

    [Test]
    public async Task Func_例外数()
    {
        int attempts = Retry.Attempts;

        try
        {
            await Retry.InvokeAsync(TestFunc);
        }
        catch (AggregateException e)
        {
            Assert.AreEqual(e.Flatten().InnerExceptions.Where(p => p is InvalidOperationException).Count(), attempts);
        }
    }

    public void TestAction()
    {
        throw new InvalidOperationException();
    }

    public bool TestFunc()
    {
        throw new InvalidOperationException();
    }
}
