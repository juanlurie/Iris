namespace Iris.Messaging
{
    public class HeaderKeys
    {
        public const string MessageType = "Iris.MessageType";
        public const string SentTime = "Iris.SentTime";
        public const string ReceivedTime = "Iris.ReceivedTime";
        public const string CompletedTime = "Iris.CompletedTime";
        public const string FirstLevelRetryCount = "Iris.Retry.Count";
        public const string SecondLevelRetryCount = "Iris.SecondLevelRetry.Count";
        public const string FailureDetails = "Iris.Failed.Exception";
        public const string TimeoutExpire = "Iris.Timeout.Expire";
        public const string RouteExpiredTimeoutTo = "Iris.Timeout.RouteExpiredTimeoutTo";
        public const string OriginalReplyToAddress = "Iris.Timeout.ReplyToAddress;";
        public const string ControlMessageHeader = "Iris.ControlMessage";
        public const string ReturnErrorCode = "Iris.ReturnErrorCode";
        public const string ProcessingEndpoint = "Iris.ProcessingEndpoint";
        public const string UserName = "Iris.UserName";
        public static string MonitoringPeriod = "Iris.MonitoringPeriod";
        public static string TotalMessagesProcessed = "Iris.TotalMessagesProcessed";
        public static string TotalErrorMessages = "Iris.TotalErrorMessages";
        public static string AverageTimeToDeliver = "Iris.AverageTimeToDeliver";
        public static string AverageTimeToProcess = "Iris.AverageTimeToProcess";
        public static string Heartbeat = "Iris.Heartbeat";
    }
}