using System;
using System.Collections.Generic;
using System.Linq;
using CatchMe.Abstraction;

namespace CatchMe;

public class CatchMeBuilder : ICatchMeBuilder
{
    private static readonly Action DefaultAction = () => { };
    private readonly Dictionary<Type, Action> _exceptions = new();
    private Action _finallyAction = DefaultAction;

    public CatchMeBuilder(Type type, Action action)
    {
        _exceptions.Add(type, action);
    }

    public ICatchMeBuilder Or<T>() where T : Exception
    {
        return Or<T>(DefaultAction);
    }

    public ICatchMeBuilder Or<T>(Action action) where T : Exception
    {
        return Or(typeof(T), action);
    }

    public ICatchMeBuilder Or(Type type)
    {
        return Or(type, DefaultAction);
    }

    public ICatchMeBuilder Finally(Action action)
    {
        _finallyAction = action;
        return this;
    }

    public ICatchMeBuilder Or(Type type, Action action)
    {
        if (!typeof(Exception).IsAssignableFrom(type))
        {
            throw new ArgumentException("Type must inherit from Exception");
        }

        _exceptions[type] = action;
        return this;
    }

    public T Execute<T>(Func<T> func)
    {
        T result = default;
        ExecuteInternal(() => result = func());
        return result;
    }

    public void Execute(Action action)
    {
        ExecuteInternal(action);
    }

    private void ExecuteInternal(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            var exceptionType = e.GetType();
            var exceptionKey = _exceptions.Keys.FirstOrDefault(p => p.IsAssignableFrom(exceptionType));
            if (exceptionKey == null)
            {
                throw;
            }

            _exceptions[exceptionKey]();
        }
        finally
        {
            _finallyAction();
        }
    }
}