using System;
using System.Runtime.Serialization;
using Iris.Messaging.Monitoring.SystemEvents;

namespace Iris.Messaging.Monitoring.DomainEvents
{
    public interface IIrisEndpointPerformanceMetricReceived : IDomainEvent
    {
        Address Endpoint { get; }
        int TotalMessagesProcessed { get; }
        int TotalErrorMessages { get; }
        TimeSpan AverageTimeToDeliver { get; }
        TimeSpan AverageTimeToProcess { get; }
        TimeSpan MonitoringPeriod { get; }
    }

    public class IrisEndpointPerformanceMetricReceived : IIrisEndpointPerformanceMetricReceived
    {
        [DataMember(Name = "Endpoint")]
        public Address Endpoint { get; protected set; }

        [DataMember(Name = "TotalMessagesProcessed")]
        public int TotalMessagesProcessed { get; protected set; }

        [DataMember(Name = "TotalErrorMessages")]
        public int TotalErrorMessages { get; protected set; }

        [DataMember(Name = "AverageTimeToDeliver")]
        public TimeSpan AverageTimeToDeliver { get; protected set; }

        [DataMember(Name = "AverageTimeToProcess")]
        public TimeSpan AverageTimeToProcess { get; protected set; }

        [DataMember(Name = "MonitoringPeriod")]
        public TimeSpan MonitoringPeriod { get; protected set; }

        public IrisEndpointPerformanceMetricReceived(EndpointPerformanceEventArgs e)
        {
            Endpoint = e.Endpoint;
            TotalMessagesProcessed = e.TotalMessagesProcessed;
            TotalErrorMessages = e.TotalErrorMessages;
            AverageTimeToDeliver = e.AverageTimeToDeliver;
            AverageTimeToProcess = e.AverageTimeToProcess;
            MonitoringPeriod = e.MonitoringPeriod;
        }
    }
}
