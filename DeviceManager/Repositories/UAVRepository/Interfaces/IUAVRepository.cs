using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Repositories.UAVRepository.Interfaces
{
    public interface IUAVRepository
    {
        Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        Task<UAV> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteUAVAsync(int tailId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default);
        Task<bool> DoesUAVExistAsync(int tailId, CancellationToken cancellationToken = default);
    }
}
