using Heritage.Linq;
using Heritage.Systems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heritage.Test;

public class DisposableTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Contains_True()
    {
        Action action = () => 
        {
            Console.WriteLine("テスト");
        };

        var disposables = new CompositeDisposable();

        disposables.Add(action);

        var result = disposables.Contains(action);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Contains_False()
    {
        Action actionA = () =>
        {
            Console.WriteLine("テスト A");
        };

        Action actionB = () =>
        {
            Console.WriteLine("テスト B");
        };

        var disposables = new CompositeDisposable();

        disposables.Add(actionA);

        var result = disposables.Contains(actionB);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Remove_True()
    {
        Action action = () =>
        {
            Console.WriteLine("テスト");
        };

        var disposables = new CompositeDisposable();

        disposables.Add(action);

        var result = disposables.Remove(action);

        Assert.That(result, Is.True);
        Assert.That(disposables.Count, Is.Zero);
    }

    [Test]
    public void Remove_False()
    {
        Action actionA = () =>
        {
            Console.WriteLine("テスト A");
        };

        Action actionB = () =>
        {
            Console.WriteLine("テスト B");
        };

        var disposables = new CompositeDisposable();

        disposables.Add(actionA);

        var result = disposables.Remove(actionB);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Enumerable()
    {
        int value = 0;

        Action actionA = () =>
        {
            value += 1;
        };

        Action actionB = () =>
        {
            value += 2;
        };

        var disposables = new CompositeDisposable();

        disposables.Add(actionA);
        disposables.Add(actionB);

        foreach (var action in disposables)
        {
            action.Dispose();
        }

        Assert.AreEqual(value, 1+2);
    }
}
