using Core.Common.Enums;
using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Repositories.SleeveRepository.Interfaces;
using DeviceManager.Services.ACMNotification.Interfaces;
using DeviceManager.Services.SimulatorNotification.Interfaces;
using DeviceManager.Services.SleeveService.Interfaces;
using DeviceManager.Services.TelemetryDeviceNotification.Interfaces;

namespace DeviceManager.Services.MongoDB.SleeveDBService
{
    public class SleeveService : ISleeveService
    {
        private readonly ISleeveRepository _sleeveRepository;
        private readonly ITelemetryDeviceNotificationService _telemetryDeviceNotificationService;
        private readonly IACMNotificationService _acmNotificationService;
        private readonly ISimulatorNotificationService _simulatorNotificationService;

        public SleeveService(
            ISleeveRepository sleeveRepository,
            ITelemetryDeviceNotificationService telemetryDeviceNotificationService,
            IACMNotificationService acmNotificationService,
            ISimulatorNotificationService simulatorNotificationService)
        {
            _sleeveRepository = sleeveRepository;
            _telemetryDeviceNotificationService = telemetryDeviceNotificationService;
            _acmNotificationService = acmNotificationService;
            _simulatorNotificationService = simulatorNotificationService;
        }

        public async Task<bool> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default)
        {
            Sleeve createdSleeve = await _sleeveRepository.CreateSleeveAsync(createSleeveDTO, cancellationToken);
            bool created = createdSleeve != null;

            if (created)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Created, createSleeveDTO.Name, cancellationToken);
                _ = _acmNotificationService.NotifySleeveChangedAsync(CrudOperation.Created, createSleeveDTO.Name, cancellationToken);
            }

            return created;
        }

        public async Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            bool deleted = await _sleeveRepository.DeleteSleeveAsync(name, cancellationToken);

            if (deleted)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Deleted, name, cancellationToken);
                _ = _acmNotificationService.NotifySleeveChangedAsync(CrudOperation.Deleted, name, cancellationToken);
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
            int? assignedTailId = null;
            if (updateSleeveDTO.PortNumbers != null)
            {
                SleeveRo currentSleeve = await _sleeveRepository.GetSleeveByNameAsync(name, cancellationToken);
                assignedTailId = currentSleeve?.AssignedToTailId;
            }

            bool updated = await _sleeveRepository.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);

            if (updated)
            {
                _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Updated, name, cancellationToken);
                _ = _acmNotificationService.NotifySleeveChangedAsync(CrudOperation.Updated, name, cancellationToken);

                if (assignedTailId.HasValue)
                {
                    _ = _simulatorNotificationService.NotifyUAVPortsChangedAsync(assignedTailId.Value, updateSleeveDTO.PortNumbers, cancellationToken);
                }
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

        public Task<bool> AssignSleeveToUavAsync(int tailId, string sleeveName, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.AssignSleeveToUavAsync(tailId, sleeveName, cancellationToken);
        }
    }
}
