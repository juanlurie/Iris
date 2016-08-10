using Iris.Ioc;
using Iris.Messaging.Bus;
using Iris.Messaging.Callbacks;
using Iris.Messaging.Monitoring;
using Iris.Messaging.Monitoring.Pipeline;
using Iris.Messaging.Pipeline;
using Iris.Messaging.Pipeline.Modules;
using Iris.Messaging.Routing;
using Iris.Messaging.Timeouts;
using Iris.Messaging.Transports;
using Iris.Pipes;
using Iris.Reflection;

namespace Iris.Messaging.Configuration
{
    internal class MessageBusDependencyRegistrar : IRegisterDependencies
    {
        public void Register(IContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Transport>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MessageBus>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<Router>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<StorageDrivenPublisher>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<CallBackManager>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<LocalBus>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<SubscriptionManager>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<Dispatcher>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<TimeoutProcessor>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<TypeMapper>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<ControlBus>(DependencyLifecycle.SingleInstance);

            containerBuilder.RegisterType<MessageErrorModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<HeartbeatService>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<AuditModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<ExtractMessagesModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MessageMutatorModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<DispatchMessagesModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<CallBackHandlerModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<MessageSerializationModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<HeaderBuilderModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<SendMessageModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<EnqueuedMessageSenderModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<PerformanceMetricsModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<HeartbeatMonitorModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<PerformanceMonitorModule>(DependencyLifecycle.SingleInstance);
            containerBuilder.RegisterType<CommandValidationModule>(DependencyLifecycle.SingleInstance);

            containerBuilder.RegisterType<UnitOfWorkModule>(DependencyLifecycle.InstancePerUnitOfWork);
            containerBuilder.RegisterType<OutgoingMessageContext>(DependencyLifecycle.InstancePerDependency);

            var outgoingPipeline = new ModulePipeFactory<OutgoingMessageContext>()                
                .Add<MessageSerializationModule>()
                .Add<MessageMutatorModule>()
                .Add<HeaderBuilderModule>()
                .Add<CommandValidationModule>()
                .Add<SendMessageModule>();

            containerBuilder.RegisterSingleton(outgoingPipeline);
        }
    }
}
