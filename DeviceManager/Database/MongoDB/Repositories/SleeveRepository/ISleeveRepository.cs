using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Repositories.SleeveRepository
{
    public interface ISleeveRepository
    {
        public Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<Sleeve> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default);
        public Task<bool> DeleteSleeveAsync(string name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default);
        public Task<bool> DoesSleeveExistsAsync(string name);
    }
}
