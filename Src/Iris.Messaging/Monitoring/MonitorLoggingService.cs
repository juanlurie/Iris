using System;
using Iris.Logging;
using Iris.Messaging.Monitoring.DomainEvents;

namespace Iris.Messaging.Monitoring
{
    public class MonitorLoggingService :
        IHandleMessage<IIrisEndpointPerformanceMetricReceived>,
        IHandleMessage<IIrisEndpointFailed>,
        IHandleMessage<IIrisEndpointResumed>
    {
        private static readonly ILog Logger = LogFactory.BuildLogger(typeof(MonitorLoggingService));

        public void Handle(IIrisEndpointPerformanceMetricReceived e)
        {
            Logger.Info("[Endpoint: {0}] [PROCESSED: {1}] [MSG/S: {1}] [ERROR: {3}] [ATTD: {4} ] [ATTP: {5}]",
                                  e.Endpoint,
                                  e.TotalMessagesProcessed,
                                  (e.TotalMessagesProcessed + e.TotalErrorMessages) / e.MonitoringPeriod.Seconds,
                                  e.TotalErrorMessages,
                                  e.AverageTimeToDeliver.Milliseconds,
                                  e.AverageTimeToProcess.Milliseconds);
        }

        public void Handle(IIrisEndpointFailed e)
        {
            Logger.Error("Endpoint {0} has not been seen since {1} and appears to be down.", e.Endpoint, e.LastSeen.ToDisplayString());
        }

        public void Handle(IIrisEndpointResumed e)
        {
            Logger.Info("Endpoint {0} resumed opperation at {1}", e.Endpoint, e.ResumedTime.ToDisplayString());
        }
    }
}
