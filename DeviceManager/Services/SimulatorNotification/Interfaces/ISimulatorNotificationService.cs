using Core.Common.Enums;

namespace DeviceManager.Services.SimulatorNotification.Interfaces
{
    public interface ISimulatorNotificationService
    {
        Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, int? newTailId = null, CancellationToken cancellationToken = default);
        Task NotifyUAVPortsChangedAsync(int tailId, IEnumerable<int> newPorts, CancellationToken cancellationToken = default);
    }
}
