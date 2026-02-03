using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.UAVRepository;
using DeviceManager.Database.MongoDB.Services.UAVDBService.Interfaces;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Services.UAVDBService
{
    public class UAVDBService : IUAVDBService
    {
        private readonly IUAVRepository _uavRepository;
        public UAVDBService(IUAVRepository _uavRepository)
        {
            this._uavRepository = _uavRepository;
        }

        public async Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default)
        {
            UAV createdUav = await _uavRepository.CreateUAVAsync(createUAVDTO, cancellationToken);
            return createdUav != null;
        }

        public Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _uavRepository.DeleteUAVAsync(tailId, cancellationToken);
        }

        public Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            return _uavRepository.GetAllUAVsAsync(cancellationToken);
        }

        public Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _uavRepository.GetUAVByTailIdAsync(tailId, cancellationToken);
        }

        public Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            return _uavRepository.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
        }
    }
}
