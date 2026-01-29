using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Repositories.Interfaces
{
    public interface IUAVRepository
    {
        public Task<UAVRo> GetUAVByTailIdAsync(int tailId,CancellationToken cancellationToken = default);
        public Task<UAV> CreateUAVAsync(CreateUAVDTO createUAVDTO,CancellationToken cancellationToken = default);
        public Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto,CancellationToken cancellationToken = default);
        public Task<bool> DeleteUAVAsync(int tailId,CancellationToken cancellationToken = default);
        public Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default);
    }
}
