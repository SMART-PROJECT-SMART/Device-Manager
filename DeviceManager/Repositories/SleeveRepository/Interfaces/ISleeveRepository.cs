using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Repositories.SleeveRepository.Interfaces
{
    public interface ISleeveRepository
    {
        Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Sleeve> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default);
        Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default);
        Task<bool> DeleteSleeveAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default);
        Task<bool> DoesSleeveExistAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default);
        Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
        Task ReassignSleeveAsync(int oldTailId, int newTailId, CancellationToken cancellationToken = default);
    }
}
