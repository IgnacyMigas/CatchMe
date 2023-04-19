using System;

namespace CatchMe;

public static class CatchMe
{
    private static Action<Exception> DefaultAction => _ => { };

    public static ICatchMeBuilder Handle<T>() where T : Exception => Handle<T>(DefaultAction);

    public static ICatchMeBuilder Handle<T>(Action<Exception> action) where T : Exception => Handle(typeof(T), action);

    public static ICatchMeBuilder Handle(Type type) => Handle(type, DefaultAction);

    public static ICatchMeBuilder Handle(Type type, Action<Exception> action)
    {
        if (!typeof(Exception).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must inherit from Exception", nameof(type));
        }

        return new CatchMeBuilder(type, action);
    }
}