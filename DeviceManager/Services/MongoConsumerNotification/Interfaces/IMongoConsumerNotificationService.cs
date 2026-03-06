using Core.Common.Enums;

namespace DeviceManager.Services.MongoConsumerNotification.Interfaces
{
    public interface IMongoConsumerNotificationService
    {
        Task NotifyUAVChangedAsync(CrudOperation operation, int tailId, int? newTailId = null, CancellationToken cancellationToken = default);
    }
}
