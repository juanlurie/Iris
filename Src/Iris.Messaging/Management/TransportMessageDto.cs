using System;
using Iris.Messaging.Transports;

namespace Iris.Messaging.Management
{
    public class TransportMessageDto
    {
        public Guid MessageId { get; set; }
        public Guid CorrelationId { get; set; }
        public HeaderValue[] Headers { get; set; }
        public string Body { get; set; }
        public string ReplyToAddress { get; set; }

        public TransportMessageDto()
        {}

        public TransportMessageDto(Guid messageId, Guid correlationId, string endpoint, string body, HeaderValue[] headers)
        {
            Mandate.ParameterNotDefaut(messageId, "messageId");
            Mandate.ParameterNotDefaut(correlationId, "correlationId");
            Mandate.ParameterNotNullOrEmpty(endpoint, "endpoint");
            Mandate.ParameterNotNullOrEmpty(body, "body");
            Mandate.ParameterNotNullOrEmpty(headers, "headers");

            MessageId = messageId;
            CorrelationId = correlationId;
            ReplyToAddress = endpoint;
            Body = body;
            Headers = headers;
        }
    }
}