﻿namespace Iris.Messaging
{
    public interface IInMemoryEventBus
    {
        void Raise<T>(T @event) where T : class;
    }
}