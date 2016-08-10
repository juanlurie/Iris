using System;
using System.Runtime.Serialization;
using Iris.Messaging.Monitoring.SystemEvents;

namespace Iris.Messaging.Monitoring.DomainEvents
{
    public interface IIrisEndpointResumed : IDomainEvent
    {
        string Endpoint { get; }
        DateTime ResumedTime { get; }
    }

    public class IrisEndpointResumed : IIrisEndpointResumed
    {
        [DataMember(Name = "")]
        public string Endpoint { get; protected set; }

        [DataMember(Name = "")]
        public DateTime ResumedTime { get; protected set; }

        public IrisEndpointResumed(EndpointHeartbeatResumedEventArgs e)
        {
            Endpoint = e.Endpoint;
            ResumedTime = e.ResumedTime;
        }
    }
}