using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.SleeveRepository;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Database.MongoDB.Services.SleeveDBService
{
    public class SleeveDBService : ISleeveDBService
    {
        private readonly ISleeveRepository _sleeveRepository;

        public SleeveDBService(ISleeveRepository sleeveRepository)
        {
            _sleeveRepository = sleeveRepository;
        }

        public async Task<bool> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default)
        {
            Sleeve createdSleeve = await _sleeveRepository.CreateSleeveAsync(createSleeveDTO, cancellationToken);
            return createdSleeve != null;
        }

        public Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.DeleteSleeveAsync(name, cancellationToken);
        }

        public Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetAllSleevesAsync(cancellationToken);
        }

        public Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetSleeveByNameAsync(name, cancellationToken);
        }

        public Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);
        }

        public Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.GetAvailableSleeveForUAVAsync(tailId, cancellationToken);
        }

        public Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            return _sleeveRepository.ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
        }
    }
}
