using System;

namespace CatchMe.Abstraction;

public interface ICatchMe
{
    ICatchMeBuilder Handle(Type type);
    ICatchMeBuilder Handle(Type type, Action action);
    ICatchMeBuilder Handle<T>();
    ICatchMeBuilder Handle<T>(Action action);
}