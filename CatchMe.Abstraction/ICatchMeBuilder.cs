using System;

namespace CatchMe.Abstraction;

public interface ICatchMeBuilder
{
    void Execute(Action action);
    ICatchMeBuilder Finally(Action action);
    ICatchMeBuilder Or(Type type);
    ICatchMeBuilder Or(Type type, Action action);
    ICatchMeBuilder Or<T>() where T : Exception;
    ICatchMeBuilder Or<T>(Action action) where T : Exception;
    T Execute<T>(Func<T> func);
}