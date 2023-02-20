using System;
using CatchMe.Abstraction;

namespace CatchMe;

public static class CatchMe // : ICatchMe
{
    private static readonly Action DefaultAction = () => { };

    public static ICatchMeBuilder Handle<T>() where T : Exception
    {
        return Handle<T>(DefaultAction);
    }

    public static ICatchMeBuilder Handle<T>(Action action) where T : Exception
    {
        return Handle(typeof(T), action);
    }

    public static ICatchMeBuilder Handle(Type type)
    {
        return Handle(type, DefaultAction);
    }

    public static ICatchMeBuilder Handle(Type type, Action action)
    {
        if (!typeof(Exception).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must inherit from Exception");
        }

        return new CatchMeBuilder(type, action);
    }
}