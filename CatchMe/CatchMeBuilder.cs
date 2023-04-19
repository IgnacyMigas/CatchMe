using System;
using System.Collections.Generic;

namespace CatchMe;

public class CatchMeBuilder : ICatchMeBuilder
{
    private static Action<Exception> DefaultAction => _ => { };
    private readonly Dictionary<Type, Action<Exception>> _exceptions = new();
    private Action _finallyAction = () => { };

    public CatchMeBuilder(Type type, Action<Exception> action)
    {
        _exceptions.Add(type, action);
    }

    public ICatchMeBuilder Or<T>() where T : Exception => Or<T>(DefaultAction);

    public ICatchMeBuilder Or<T>(Action<Exception> action) where T : Exception => Or(typeof(T), action);

    public ICatchMeBuilder Or(Type type) => Or(type, DefaultAction);

    public ICatchMeBuilder Finally(Action action)
    {
        _finallyAction = action;
        return this;
    }

    public ICatchMeBuilder Or(Type type, Action<Exception> action)
    {
        if (!typeof(Exception).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must inherit from Exception", nameof(type));
        }

        _exceptions[type] = action;
        return this;
    }

    public T Execute<T>(Func<T> func)
    {
        T result = default!;
        ExecuteInternal(() => result = func());
        return result;
    }

    public void Execute(Action action) => ExecuteInternal(action);

    private void ExecuteInternal(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e) when (_exceptions.TryGetValue(e.GetType(), out var catchAction))
        {
            catchAction(e);
        }
        finally
        {
            _finallyAction();
        }
    }
}