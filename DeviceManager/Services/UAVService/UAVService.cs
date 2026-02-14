using Core.Common.Enums;
using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Repositories.SleeveRepository.Interfaces;
using DeviceManager.Repositories.UAVRepository.Interfaces;
using DeviceManager.Services.Kafka;
using DeviceManager.Services.SimulatorNotification.Interfaces;
using DeviceManager.Services.UAVDBService.Interfaces;

namespace DeviceManager.Services.UAVDBService
{
    public class UAVService : IUAVService
    {
        private readonly IUAVRepository _uavRepository;
        private readonly ISleeveRepository _sleeveRepository;
        private readonly ISimulatorNotificationService _simulatorNotificationService;
        private readonly IKafkaTopicManager _kafkaTopicManager;

        public UAVService(
            IUAVRepository uavRepository,
            ISleeveRepository sleeveRepository,
            ISimulatorNotificationService simulatorNotificationService,
            IKafkaTopicManager kafkaTopicManager)
        {
            _uavRepository = uavRepository;
            _sleeveRepository = sleeveRepository;
            _simulatorNotificationService = simulatorNotificationService;
            _kafkaTopicManager = kafkaTopicManager;
        }

        public async Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default)
        {
            UAV createdUav = await _uavRepository.CreateUAVAsync(createUAVDTO, cancellationToken);
            bool created = createdUav != null;

            if (created)
            {
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Created, createUAVDTO.TailId, cancellationToken: cancellationToken);
                _ = _kafkaTopicManager.CreateTopicAsync(createUAVDTO.TailId, cancellationToken);
            }

            return created;
        }

        public async Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            bool deleted = await _uavRepository.DeleteUAVAsync(tailId, cancellationToken);

            if (deleted)
            {
                _ = _sleeveRepository.ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Deleted, tailId, cancellationToken: cancellationToken);
                _ = _kafkaTopicManager.DeleteTopicAsync(tailId, cancellationToken);
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
                int? newTailId = updateUAVDto.TailId.HasValue && updateUAVDto.TailId.Value != tailId
                    ? updateUAVDto.TailId
                    : null;
                if (newTailId.HasValue)
                {
                    _ = _sleeveRepository.ReassignSleeveAsync(tailId, newTailId.Value, cancellationToken);
                }
                _ = _simulatorNotificationService.NotifyUAVChangedAsync(CrudOperation.Updated, tailId, newTailId, cancellationToken);
                _ = _kafkaTopicManager.UpdateTopicAsync(tailId, newTailId, cancellationToken);
            }

            return updated;
        }
    }
}
