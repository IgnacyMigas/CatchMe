using NUnit.Framework;

namespace CatchMe.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void ShouldCatchExactException()
    {
        var isExecuted = false;
        var testClass = CatchMe.Handle<SealedException>(() => isExecuted = true);

        //WHEN
        testClass.Execute(() => throw new SealedException());

        //THEN
        Assert.That(isExecuted, Is.True);
    }

    [Test]
    public void ShouldCatchDerivedException()
    {
        var isExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(() => isExecuted = true);

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        Assert.That(isExecuted, Is.True);
    }

    [Test]
    public void ShouldCatchBaseWhenFirst()
    {
        //GIVEN
        var isExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(() => isExecuted = true).Or<DerivedException>();

        //WHEN
        testClass.Execute(() => throw new BaseException());

        //THEN
        Assert.That(isExecuted, Is.True);
    }

    [Test]
    public void ShouldCatchDerivedWhenFirst()
    {
        //GIVEN
        var isExecuted = false;
        var testClass = CatchMe.Handle<DerivedException>(() => isExecuted = true).Or<BaseException>();

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        Assert.That(isExecuted, Is.True);
    }

    [Test]
    public void ShouldCatchDerivedOnBaseWhenFirst()
    {
        //GIVEN
        var isExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(() => isExecuted = true).Or<DerivedException>();

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        Assert.That(isExecuted, Is.True);
    }

    [Test]
    public void ShouldThrowWhenNotCoveredException()
    {
        var isExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(() => isExecuted = true);

        //WHEN-THEN
        Assert.Throws<SealedException>(() => testClass.Execute(() => throw new SealedException()));
    }

    [Test]
    public void ShouldThrowWhenNotInheritFromException()
    {
        //WHEN-THEN
        Assert.Throws<ArgumentException>(() => CatchMe.Handle(typeof(int)));
        Assert.Throws<ArgumentException>(() => CatchMe.Handle<Exception>().Or(typeof(int)));
    }
}