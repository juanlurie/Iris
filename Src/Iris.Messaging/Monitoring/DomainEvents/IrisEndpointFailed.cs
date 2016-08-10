using System;
using System.Runtime.Serialization;
using Iris.Messaging.Monitoring.SystemEvents;

namespace Iris.Messaging.Monitoring.DomainEvents
{
    public interface IIrisEndpointFailed : IDomainEvent
    {
        string Endpoint { get; }
        DateTime LastSeen { get; }
    }

    [DataContract]
    public class IrisEndpointFailed : IIrisEndpointFailed
    {
        [DataMember(Name = "")]
        public string Endpoint { get; protected set; }

        [DataMember(Name = "")]
        public DateTime LastSeen { get; protected set; }

        public IrisEndpointFailed(EndpointHeartbeatStoppedEventArgs e)
        {
            Endpoint = e.Endpoint;
            LastSeen = e.LastSeen;
        }
    }
}