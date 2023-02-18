namespace CatchMe.Tests
{
    internal class BaseException : Exception
    {
    }

    internal class DerivedException : BaseException
    {

    }

    internal sealed class SealedException : Exception { }
}
