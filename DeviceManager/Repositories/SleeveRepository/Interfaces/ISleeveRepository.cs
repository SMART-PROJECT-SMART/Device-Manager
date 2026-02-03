using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Repositories.SleeveRepository.Interfaces
{
    public interface ISleeveRepository
    {
        public Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<Sleeve> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> DeleteSleeveAsync(string name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default);
        public Task<bool> DoesSleeveExistsAsync(string name);
        public Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default);
        public Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
    }
}
