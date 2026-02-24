using Core.Common.Enums;

namespace DeviceManager.Services.ACMNotification.Interfaces
{
    public interface IACMNotificationService
    {
        Task NotifySleeveChangedAsync(CrudOperation operation, int id, string name, CancellationToken cancellationToken = default);
    }
}
