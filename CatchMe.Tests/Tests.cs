using NUnit.Framework;

namespace CatchMe.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ShouldCatchBaseWhenFirst()
        {
            //GIVEN
            var isExecuted = false;
            var testClass = CatchMe.Handle<BaseException>(() => isExecuted = true).Or<DerivedException>();

            //WHEN
            testClass.Execute(() => throw new BaseException());

            //THEN
            Assert.IsTrue(isExecuted);
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
            Assert.IsTrue(isExecuted);
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
            Assert.IsTrue(isExecuted);
        }

        [Test]
        public void ShoudThrowWhenNotInheritFromException()
        {
            //WHEN-THEN
            Assert.Throws<ArgumentException>(() => CatchMe.Handle(typeof(int)));
            Assert.Throws<ArgumentException>(() => CatchMe.Handle<Exception>().Or(typeof(int)));
        }
    }
}