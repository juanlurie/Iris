using Iris.Ioc;
using Iris.Messaging.Monitoring.DomainEvents;
using Iris.Messaging.Monitoring.Pipeline;
using Iris.Messaging.Monitoring.SystemEvents;
using Iris.Messaging.Pipeline;
using Iris.Messaging.Pipeline.Modules;
using Iris.Pipes;

namespace Iris.Messaging.EndPoints
{
    public abstract class MonitoringEndpoint<TContainerBuilder> : ControlEndpoint<TContainerBuilder> 
        where TContainerBuilder : IContainerBuilder, new()
    {
        protected override void ConfigurePipeline(TContainerBuilder containerBuilder)
        {
            var incomingPipeline = new ModulePipeFactory<IncomingMessageContext>()
                .Add<EnqueuedMessageSenderModule>()
                .Add<HeartbeatMonitorModule>()
                .Add<PerformanceMonitorModule>()
                .Add<DispatchMessagesModule>();

            containerBuilder.RegisterSingleton(incomingPipeline);

            HeartbeatMonitorModule.OnEndpointHeartbeatStopped += OnEndpointHeartbeatStopped;
            HeartbeatMonitorModule.OnEndpointHeartbeatResumed += OnEndpointHeartbeatResumed;
            PerformanceMonitorModule.OnEndpointPerformanceMetricReceived += OnEndpointPerformanceMetricReceived;
        }

        private void OnEndpointPerformanceMetricReceived(EndpointPerformanceEventArgs e, object sender)
        {
            IDomainEvent metricReceived = new IrisEndpointPerformanceMetricReceived(e);
            RaiseEvent(metricReceived);
        }

        private void OnEndpointHeartbeatStopped(EndpointHeartbeatStoppedEventArgs e, object sender)
        {
            IDomainEvent endpointFailed = new IrisEndpointFailed(e);
            RaiseEvent(endpointFailed);
        }

        private void OnEndpointHeartbeatResumed(EndpointHeartbeatResumedEventArgs e, object sender)
        {
            IDomainEvent endpointFailed = new IrisEndpointResumed(e);
            RaiseEvent(endpointFailed);
        }
    }
}