using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Services.UAVDBService.Interfaces
{
    public interface IUAVService
    {
        public Task<UAVRo> GetUAVByTailIdAsync(int tailId,CancellationToken cancellationToken = default);
        public Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default);
        public Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default);
        public Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default);
    }
}
