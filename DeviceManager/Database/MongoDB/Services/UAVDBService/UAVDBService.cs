using Core.Common.Enums;
using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.UAVRepository;
using DeviceManager.Database.MongoDB.Services.UAVDBService.Interfaces;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.SimulatorNotification;

namespace DeviceManager.Database.MongoDB.Services.UAVDBService
{
    public class UAVDBService : IUAVDBService
    {
        private readonly IUAVRepository _uavRepository;
        private readonly ISimulatorNotificationService _simulatorNotificationService;

        public UAVDBService(
            IUAVRepository uavRepository,
            ISimulatorNotificationService simulatorNotificationService)
        {
            _uavRepository = uavRepository;
            _simulatorNotificationService = simulatorNotificationService;
        }

        public async Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default)
        {
            UAV createdUav = await _uavRepository.CreateUAVAsync(createUAVDTO, cancellationToken);
            bool created = createdUav != null;

            if (created)
            {
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Created, createUAVDTO.TailId, cancellationToken);
            }

            return created;
        }

        public async Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            bool deleted = await _uavRepository.DeleteUAVAsync(tailId, cancellationToken);

            if (deleted)
            {
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Deleted, tailId, cancellationToken);
            }

            return deleted;
        }

        public Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            return _uavRepository.GetAllUAVsAsync(cancellationToken);
        }

        public Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _uavRepository.GetUAVByTailIdAsync(tailId, cancellationToken);
        }

        public async Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            bool updated = await _uavRepository.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);

            if (updated)
            {
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Updated, tailId, cancellationToken);
            }

            return updated;
        }
    }
}
