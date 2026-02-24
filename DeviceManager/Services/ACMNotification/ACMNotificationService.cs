using Core.Common.Enums;
using DeviceManager.Models.Config;
using DeviceManager.Models.Dto;
using DeviceManager.Services.ACMNotification.Interfaces;
using Microsoft.Extensions.Options;

namespace DeviceManager.Services.ACMNotification
{
    public class ACMNotificationService : IACMNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly ACMConfiguration _acmConfig;

        public ACMNotificationService(HttpClient httpClient, IOptions<ACMConfiguration> acmConfig)
        {
            _httpClient = httpClient;
            _acmConfig = acmConfig.Value;
        }

        public async Task NotifySleeveChangedAsync(CrudOperation operation, int id, string name, CancellationToken cancellationToken = default)
        {
            SleeveChangedNotificationDTO notification = new SleeveChangedNotificationDTO(operation, id, name);
            string url = new Uri(new Uri(_acmConfig.BaseUrl), _acmConfig.WebhookEndpoint).ToString();
            await _httpClient.PostAsJsonAsync(url, notification, cancellationToken);
        }
    }
}
