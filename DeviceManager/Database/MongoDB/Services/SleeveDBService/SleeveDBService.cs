using Core.Common.Enums;
using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.SleeveRepository;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.TelemetryDeviceNotification;

namespace DeviceManager.Database.MongoDB.Services.SleeveDBService
{
    public class SleeveDBService : ISleeveDBService
    {
        private readonly ISleeveRepository _sleeveRepository;
        private readonly ITelemetryDeviceNotificationService _telemetryDeviceNotificationService;

        public SleeveDBService(
            ISleeveRepository sleeveRepository,
            ITelemetryDeviceNotificationService telemetryDeviceNotificationService)
        {
            _sleeveRepository = sleeveRepository;
            _telemetryDeviceNotificationService = telemetryDeviceNotificationService;
        }

        public async Task<bool> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default)
        {
            Sleeve createdSleeve = await _sleeveRepository.CreateSleeveAsync(createSleeveDTO, cancellationToken);
            bool created = createdSleeve != null;

            if (created)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Created, createSleeveDTO.Name, cancellationToken);
            }

            return created;
        }

        public async Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            bool deleted = await _sleeveRepository.DeleteSleeveAsync(name, cancellationToken);

            if (deleted)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Deleted, name, cancellationToken);
            }

            return deleted;
        }

        public Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetAllSleevesAsync(cancellationToken);
        }

        public Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetSleeveByNameAsync(name, cancellationToken);
        }

        public async Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default)
        {
            bool updated = await _sleeveRepository.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);

            if (updated)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Updated, name, cancellationToken);
            }

            return updated;
        }

        public Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetAvailableSleeveForUAVAsync(tailId, cancellationToken);
        }

        public Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
        }
    }
}
