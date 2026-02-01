using Core.Common.Enums;
using DeviceManager.Config;
using DeviceManager.Models.Dto;
using Microsoft.Extensions.Options;

namespace DeviceManager.Services.TelemetryDeviceNotification
{
    public class TelemetryDeviceNotificationService : ITelemetryDeviceNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly TelemetryDeviceConfiguration _telemetryDeviceConfig;

        public TelemetryDeviceNotificationService(HttpClient httpClient, IOptions<TelemetryDeviceConfiguration> telemetryDeviceConfig)
        {
            _httpClient = httpClient;
            _telemetryDeviceConfig = telemetryDeviceConfig.Value;
        }

        public async Task NotifySleeveChangedAsync(CrudOperation operation, string name, CancellationToken cancellationToken = default)
        {
            SleeveChangedNotificationDTO notification = new SleeveChangedNotificationDTO(operation, name);
            string url = $"{_telemetryDeviceConfig.BaseUrl}{_telemetryDeviceConfig.WebhookEndpoint}";
            await _httpClient.PostAsJsonAsync(url, notification, cancellationToken);
        }
    }
}
