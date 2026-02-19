using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Services.SleeveService.Interfaces
{
    public interface ISleeveService
    {
        Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default);
        Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default);
        Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        Task<bool> AssignSleeveToUavAsync(int tailId, string sleeveName, CancellationToken cancellationToken = default);
    }
}
