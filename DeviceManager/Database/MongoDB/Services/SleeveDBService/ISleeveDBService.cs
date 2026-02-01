using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Services.SleeveDBService
{
    public interface ISleeveDBService
    {
        public Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<bool> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default);
        public Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default);
    }
}
