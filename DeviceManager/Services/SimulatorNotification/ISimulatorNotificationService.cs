using Core.Common.Enums;

namespace DeviceManager.Services.SimulatorNotification
{
    public interface ISimulatorNotificationService
    {
        Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, CancellationToken cancellationToken = default);
    }
}
