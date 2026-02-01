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

        public async Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            bool isDeleted = await uavRepository.DeleteUAVAsync(tailId, cancellationToken);
            return isDeleted;
        }

        public async Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<UAVRo> uavList = await uavRepository.GetAllUAVsAsync(cancellationToken);
            return uavList;
        }

        public async Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            UAVRo uav = await uavRepository.GetUAVByTailIdAsync(tailId, cancellationToken);
            return uav;
        }

        public async Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            bool isUpdated = await uavRepository.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
            return isUpdated;
        }
    }
}
