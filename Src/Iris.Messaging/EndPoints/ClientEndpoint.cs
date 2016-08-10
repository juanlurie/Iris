using System;
using System.Reflection;
using Iris.Ioc;
using Iris.Messaging.Configuration;
using Iris.Messaging.Pipeline;
using Iris.Messaging.Pipeline.Modules;
using Iris.Pipes;

namespace Iris.Messaging.EndPoints
{
    public abstract class ClientEndpoint<TContainerBuilder> : IDisposable
        where TContainerBuilder : IContainerBuilder, new()
    {
        private readonly Configure configuration;
        private bool disposed;

        public IMessageBus MessageBus { get { return Settings.RootContainer.GetInstance<IMessageBus>(); } }

        protected ClientEndpoint()
        {
            var containerBuilder = new TContainerBuilder();
            string endpointName = Assembly.GetAssembly(GetType()).GetName().Name;            
            configuration = Configure.Initialize(endpointName, containerBuilder);
            ConfigureEndpoint(configuration);
            ConfigurePipeline(containerBuilder);

            Settings.IsClientEndpoint = true;
            Settings.FlushQueueOnStartup = true;
            Settings.RootContainer = containerBuilder.BuildContainer();
        }

        protected abstract void ConfigureEndpoint(IConfigureEndpoint configuration);

        protected virtual void ConfigurePipeline(TContainerBuilder containerBuilder)
        {
            var incomingPipeline = new ModulePipeFactory<IncomingMessageContext>()
                    .Add<EnqueuedMessageSenderModule>()
                    .Add<MessageErrorModule>()
                    .Add<ExtractMessagesModule>()
                    .Add<MessageMutatorModule>()
                    .Add<UnitOfWorkModule>()
                    .Add<DispatchMessagesModule>()
                    .Add<CallBackHandlerModule>();

            containerBuilder.RegisterSingleton(incomingPipeline);
        }

        public void Start()
        {
            configuration.Start();
        }

        public void Stop()
        {
            configuration.Stop();
        }

        ~ClientEndpoint()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                configuration.Stop();
            }

            disposed = true;
        }
    }
}
