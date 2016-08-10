using System;
using Iris.Messaging.Bus;
using Iris.Messaging.Configuration;
using Iris.Messaging.Transports;

namespace Iris.Messaging.Monitoring
{
    public class HeartbeatService : ScheduledWorkerService
    {
        private readonly ControlBus controlBus;

        public HeartbeatService(ControlBus controlBus)
        {
            this.controlBus = controlBus;
            this.SetSchedule(TimeSpan.FromSeconds(30));
        }

        public override void Start()
        {
            if (Settings.DisableHeartbeatService)
                return;

            base.Start();
        }

        protected override void DoWork()
        {
            var headers = new[]
            {
                new HeaderValue(HeaderKeys.Heartbeat, Address.Local.ToString()),
            };

            controlBus.Send(Settings.MonitoringEndpoint, headers);
        }
    }
}