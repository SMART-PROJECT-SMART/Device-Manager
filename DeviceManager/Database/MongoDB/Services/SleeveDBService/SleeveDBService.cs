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

        public async Task<bool> DeleteSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            bool isDeleted = await _sleeveRepository.DeleteSleeveAsync(name, cancellationToken);
            return isDeleted;
        }

        public async Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<SleeveRo> sleeveList = await _sleeveRepository.GetAllSleevesAsync(cancellationToken);
            return sleeveList;
        }

        public async Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            SleeveRo sleeve = await _sleeveRepository.GetSleeveByNameAsync(name, cancellationToken);
            return sleeve;
        }

        public async Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default)
        {
            bool isUpdated = await _sleeveRepository.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);
            return isUpdated;
        }
    }
}
