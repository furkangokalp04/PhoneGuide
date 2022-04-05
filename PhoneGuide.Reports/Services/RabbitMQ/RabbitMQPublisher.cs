using PhoneGuide.Shared.Dtos;
using System.Text;
using System.Text.Json;

namespace PhoneGuide.Reports.Services.RabbitMQ
{

    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(GenerateExcelMessageDto generateExcelMessageDto)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(generateExcelMessageDto);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingExcel,false, basicProperties: properties, body: bodyByte);

        }
    }
}