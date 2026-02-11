using Core.Common.Enums;
using DeviceManager.Models.Config;
using DeviceManager.Models.Dto;
using DeviceManager.Services.SimulatorNotification.Interfaces;
using Microsoft.Extensions.Options;

namespace DeviceManager.Services.SimulatorNotification
{
    public class SimulatorNotificationService : ISimulatorNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly SimulatorConfiguration _simulatorConfig;

        public SimulatorNotificationService(HttpClient httpClient, IOptions<SimulatorConfiguration> simulatorConfig)
        {
            _httpClient = httpClient;
            _simulatorConfig = simulatorConfig.Value;
        }

        public async Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, int? newTailId = null, CancellationToken cancellationToken = default)
        {
            UAVChangedNotificationDTO notification = new UAVChangedNotificationDTO(operation, tailId, newTailId);
            string url = new Uri(new Uri(_simulatorConfig.BaseUrl), _simulatorConfig.WebhookEndpoint).ToString();
            await _httpClient.PostAsJsonAsync(url, notification, cancellationToken);
        }
    }
}
