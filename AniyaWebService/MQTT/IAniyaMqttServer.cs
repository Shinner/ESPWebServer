namespace AniyaWebService.MQTT
{
    public interface IAniyaMqttServer
    {
        Task RunServer(bool enableLog);
        Task PublishMessageFromBroker(string client, string topic, string payload);
    }
}
