using MQTTnet;
using MQTTnet.Server;

namespace AniyaWebService.MQTT
{
    public class AniyaMqttServer : IAniyaMqttServer, IDisposable
    {
        MqttServer mqttServer;
        public void Dispose()
        {
           mqttServer?.Dispose();
        }

        public async Task PublishMessageFromBroker(string client, string topic, string payload)
        {
            
                // Create a new message using the builder as usual.
                var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload).Build();

                // Now inject the new message at the broker.
                await mqttServer.InjectApplicationMessage(
                    new InjectedMqttApplicationMessage(message)
                    {
                        SenderClientId = client
                    });
            
        }

        public async Task RunServer(bool enableLog)
        {
            MqttFactory mqttFactory;

            if(enableLog)
            {
                mqttFactory = new MqttFactory(new ConsoleLogger());
            }
            else
            {
                mqttFactory = new MqttFactory();
            }

            var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

            using (mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions))
            {
                await mqttServer.StartAsync();

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

                // Stop and dispose the MQTT server if it is no longer needed!
                await mqttServer.StopAsync();
            }
        }
    }
}
