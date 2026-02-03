using Core.Common.Enums;

namespace DeviceManager.Services.TelemetryDeviceNotification.Interfaces
{
    public interface ITelemetryDeviceNotificationService
    {
        Task NotifySleeveChangedAsync(CrudOperation operation, string name, CancellationToken cancellationToken = default);
    }
}
