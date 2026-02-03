using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.UAVRepository;
using DeviceManager.Database.MongoDB.Services.UAVDBService.Interfaces;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Services.UAVDBService
{
    public class UAVDBService : IUAVDBService
    {
        private readonly IUAVRepository uavRepository;
        public UAVDBService(IUAVRepository uavRepository)
        {
            this.uavRepository = uavRepository;
        }

        public async Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default)
        {
            UAV createdUav = await uavRepository.CreateUAVAsync(createUAVDTO, cancellationToken);
            return createdUav != null;
        }

        public Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return uavRepository.DeleteUAVAsync(tailId, cancellationToken);
        }

        public Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            return uavRepository.GetAllUAVsAsync(cancellationToken);
        }

        public Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return uavRepository.GetUAVByTailIdAsync(tailId, cancellationToken);
        }

        public Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            return uavRepository.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
        }
    }
}
