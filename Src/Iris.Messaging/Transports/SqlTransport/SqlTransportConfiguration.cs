using System.Configuration;
using Iris.Ioc;
using Iris.Messaging.Configuration;
using Iris.Messaging.Management;
using Iris.Messaging.Storage.MsSql;

namespace Iris.Messaging.Transports.SqlTransport
{
    public static class SqlTransportConfiguration
    {
        public const string MessagingConnectionStringKey = "Iris.Transports.SqlServer.ConnectionString";

        public static IConfigureEndpoint UseSqlTransport(this IConfigureEndpoint config)
        {
            return UseSqlTransport(config, "SqlTransport");
        }

        public static IConfigureEndpoint UseSqlTransport(this IConfigureEndpoint config, string connectionStringName)
        {
            Address.IgnoreMachineName();

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            Settings.AddSetting(MessagingConnectionStringKey, connectionString);

            if (Settings.IsClientEndpoint)
            {
                Address.InitializeLocalAddress(Address.Local.Queue + "." + Address.Local.Machine);
            }

            if (Settings.UseMySql)
                config.RegisterDependencies(new MySqlMessagingDependencyRegistrar());
            else
                config.RegisterDependencies(new SqlMessagingDependencyRegistrar());

            return config;
        }

        private class SqlMessagingDependencyRegistrar : IRegisterDependencies
        {
            public void Register(IContainerBuilder containerBuilder)
            {
                containerBuilder.RegisterType<SqlQueueCreator>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<SqlTimeoutStorage>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<SqlMessageDequeStrategy>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<SqlSubscriptionStorage>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<SqlErrorQueueManager>(DependencyLifecycle.SingleInstance);

                containerBuilder.RegisterType<PollingReceiver>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<SqlMessageSender>(DependencyLifecycle.SingleInstance);
            }
        }

        private class MySqlMessagingDependencyRegistrar : IRegisterDependencies
        {
            public void Register(IContainerBuilder containerBuilder)
            {
                containerBuilder.RegisterType<MySqlQueueCreator>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<MySqlTimeoutStorage>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<MySqlMessageDequeStrategy>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<MySqlSubscriptionStorage>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<MySqlErrorQueueManager>(DependencyLifecycle.SingleInstance);
                containerBuilder.RegisterType<MySqlMessageSender>(DependencyLifecycle.SingleInstance);

                containerBuilder.RegisterType<PollingReceiver>(DependencyLifecycle.SingleInstance);
            }
        }
    }
}
