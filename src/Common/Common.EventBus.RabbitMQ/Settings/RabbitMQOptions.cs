namespace Common.EventBus.RabbitMQ.Settings
{
        public class RabbitMQOptions
        {
                public const string EventBusConnection = "EventBusConnection";
                public string Host { get; set; }
                public string ServiceName { get; set; }
        }
}