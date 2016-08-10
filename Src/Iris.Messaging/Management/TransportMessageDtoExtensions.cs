using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Messaging.Management
{
    public static class TransportMessageDtoExtensions
    {
        public static TransportMessage ToTransportMessage(this TransportMessageDto dto, Dictionary<string, string> headers)
        {
            Address replyToAddress = Address.Parse(dto.ReplyToAddress);
            byte[] body = Encoding.UTF8.GetBytes(dto.Body);

            var message = new TransportMessage(dto.MessageId, dto.CorrelationId, replyToAddress, TimeSpan.MaxValue, headers, body);

            message.Headers.Remove(HeaderKeys.FirstLevelRetryCount);
            message.Headers.Remove(HeaderKeys.SecondLevelRetryCount);
            message.Headers.Remove(HeaderKeys.FailureDetails);

            return message;
        }
    }
}