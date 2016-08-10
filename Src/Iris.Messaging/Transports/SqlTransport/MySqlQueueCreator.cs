using System;
using System.Data.SqlClient;
using Iris.Messaging.Configuration;

namespace Iris.Messaging.Transports.SqlTransport
{
    public class MySqlQueueCreator : ICreateMessageQueues
    {
        private readonly string connectionString;

        public MySqlQueueCreator()
        {
            connectionString = Settings.GetSetting(SqlTransportConfiguration.MessagingConnectionStringKey);
        }

        public void CreateQueueIfNecessary(Address address)
        {
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    using (var command = new SqlCommand(String.Format(SqlCommands.CreateQueue, address), connection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        public void Purge(Address address)
        {
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    using (var command = new SqlCommand(String.Format(SqlCommands.PurgeQueue, address), connection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //}
        }
    }
}