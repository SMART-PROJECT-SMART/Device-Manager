using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Services.UAVDBService.Interfaces
{
    public interface IUAVService
    {
        Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        Task<bool> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default);
    }
}
