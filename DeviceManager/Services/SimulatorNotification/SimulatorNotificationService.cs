using Core.Common.Enums;
using DeviceManager.Config;
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

        public async Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, CancellationToken cancellationToken = default)
        {
            UAVChangedNotificationDTO notification = new UAVChangedNotificationDTO(operation, tailId);
            string url = $"{_simulatorConfig.BaseUrl}{_simulatorConfig.WebhookEndpoint}";
            await _httpClient.PostAsJsonAsync(url, notification, cancellationToken);
        }
    }
}
