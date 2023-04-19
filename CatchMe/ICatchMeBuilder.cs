using System;

namespace CatchMe;

public interface ICatchMeBuilder
{
    void Execute(Action action);
    ICatchMeBuilder Finally(Action action);
    ICatchMeBuilder Or(Type type);
    ICatchMeBuilder Or(Type type, Action<Exception> action);
    ICatchMeBuilder Or<T>() where T : Exception;
    ICatchMeBuilder Or<T>(Action<Exception> action) where T : Exception;
    T Execute<T>(Func<T> func);
}