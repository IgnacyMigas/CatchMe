using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace CatchMe.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void ShouldCatchExactException()
    {
        var isExecuted = false;
        var testClass = CatchMe.Handle<SealedException>(_ => isExecuted = true);

        //WHEN
        testClass.Execute(() => throw new SealedException());

        //THEN
        isExecuted.Should().BeTrue();
    }

    [Test]
    public void ShouldCatchDerivedException()
    {
        var isExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(_ => isExecuted = true);

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        isExecuted.Should().BeTrue();
    }

    [Test]
    public void ShouldCatchBaseWhenFirst()
    {
        //GIVEN
        var isBaseExecuted = false;
        var isDerivedExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(_ => isBaseExecuted = true).Or<DerivedException>(_ => isDerivedExecuted = true);

        //WHEN
        testClass.Execute(() => throw new BaseException());

        //THEN
        using (new AssertionScope())
        {
            isBaseExecuted.Should().BeTrue();
            isDerivedExecuted.Should().BeFalse();
        }
    }

    [Test]
    public void ShouldCatchDerivedWhenFirst()
    {
        //GIVEN
        var isBaseExecuted = false;
        var isDerivedExecuted = false;
        var testClass = CatchMe.Handle<DerivedException>(_ => isDerivedExecuted = true).Or<BaseException>(_ => isBaseExecuted = true);

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        using (new AssertionScope())
        {
            isBaseExecuted.Should().BeFalse();
            isDerivedExecuted.Should().BeTrue();
        }
    }

    [Test]
    public void ShouldCatchDerivedOnBaseWhenFirst()
    {
        //GIVEN
        var isBaseExecuted = false;
        var isDerivedExecuted = false;
        var testClass = CatchMe.Handle<BaseException>(_ => isBaseExecuted = true).Or<DerivedException>(_ => isDerivedExecuted = true);

        //WHEN
        testClass.Execute(() => throw new DerivedException());

        //THEN
        using (new AssertionScope())
        {
            isBaseExecuted.Should().BeTrue();
            isDerivedExecuted.Should().BeFalse();
        }
    }

    [Test]
    public void ShouldThrowWhenNotCoveredException()
    {
        //GIVEN
        var testClass = CatchMe.Handle<BaseException>();

        //WHEN-THEN
        var testDelegate = () => testClass.Execute(() => throw new SealedException());
        testDelegate.Should().Throw<SealedException>();
    }

    [Test]
    public void ShouldThrowWhenNotInheritFromException()
    {
        //GIVEN
        var intType = () => CatchMe.Handle(typeof(int));
        var orIntType = () => CatchMe.Handle<Exception>().Or(typeof(int));

        //WHEN-THEN
        intType.Should().Throw<SealedException>();
        orIntType.Should().Throw<SealedException>();
    }
}