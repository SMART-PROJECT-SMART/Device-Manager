using Core.Common.Enums;
using DeviceManager.Models.Config;
using DeviceManager.Models.Dto;
using DeviceManager.Services.MongoConsumerNotification.Interfaces;
using Microsoft.Extensions.Options;

namespace DeviceManager.Services.MongoConsumerNotification
{
    public class MongoConsumerNotificationService : IMongoConsumerNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly MongoConsumerConfiguration _mongoConsumerConfig;

        public MongoConsumerNotificationService(HttpClient httpClient, IOptions<MongoConsumerConfiguration> mongoConsumerConfig)
        {
            _httpClient = httpClient;
            _mongoConsumerConfig = mongoConsumerConfig.Value;
        }

        public async Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, int? newTailId = null, CancellationToken cancellationToken = default)
        {
            UAVChangedNotificationDTO notification = new UAVChangedNotificationDTO(operation, tailId, newTailId);
            string url = new Uri(new Uri(_mongoConsumerConfig.BaseUrl), _mongoConsumerConfig.WebhookEndpoint).ToString();
            await _httpClient.PostAsJsonAsync(url, notification, cancellationToken);
        }
    }
}
