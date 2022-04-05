using ClosedXML.Excel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PhoneGuide.GenerateExcel.WorkerService.Models;
using PhoneGuide.GenerateExcel.WorkerService.Services.RabbitMQ;
using PhoneGuide.Shared.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneGuide.GenerateExcel.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly RabbitMQClientService _rabbitMQClientService;

        private readonly IServiceProvider _serviceProvider;

        private IModel _channel;
        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);

            consumer.Received += Consumer_Received;

            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {

            var generateExcelMessage = JsonSerializer.Deserialize<GenerateExcelMessageDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            using var ms = new MemoryStream();

            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable("files", generateExcelMessage.FileId));
            
            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);

            MultipartFormDataContent multipartFormDataContent = new();

            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            var baseUrl = "http://localhost:5000/services/reports/files";

            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.PostAsync($"{baseUrl}?fileId={generateExcelMessage.FileId}", multipartFormDataContent);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"File ( Id : {generateExcelMessage.FileId}) was created by successful");
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }

        }

        private DataTable GetTable(string tableName,string fileId)
        {

            List<ReportContent> reportData = new();
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PhoneGuidedbContext>();

                 reportData = (from contactInfo in context.ContactInfos 
                                 group contactInfo by contactInfo.Location into g
                                 select new ReportContent
                                 {
                                 Location = g.Key,
                                 ContactCount = g.Select(x => x.ContactId).Distinct().Count(),
                                 PhoneNumberCount = g.Select(x => x.PhoneNumber).Distinct().Count()
                                 }).ToList();
            }

            DataTable table = new DataTable { TableName = tableName };

            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("Contact Count", typeof(int)); 
            table.Columns.Add("Phone Number Count", typeof(int));


            reportData.ForEach(x =>
            {
                table.Rows.Add(x.Location, x.ContactCount, x.PhoneNumberCount);
            });

            return table;
        }
    }
}
