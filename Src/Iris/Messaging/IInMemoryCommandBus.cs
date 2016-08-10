﻿namespace Iris.Messaging
{
    public interface IInMemoryCommandBus
    {
        void Execute<T>(T command) where T : class;
    }
}
