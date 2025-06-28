// GameEvent.cs
using System;
using System.Collections.Generic;

public class GameEvent<T>
{
    private readonly List<Action<T>> listeners = new();

    public void Subscribe(Action<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void Unsubscribe(Action<T> listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    public void Invoke(T data)
    {
        foreach (var listener in listeners.ToArray())
            listener.Invoke(data);
    }
}
