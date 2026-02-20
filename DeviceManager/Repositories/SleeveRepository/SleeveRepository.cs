using DeviceManager.Common.Constants;
using DeviceManager.Extentions;
using DeviceManager.Models;
using DeviceManager.Models.Config;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Repositories.SleeveRepository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeviceManager.Services.SleeveRepository
{
    public class SleeveRepository : ISleeveRepository
    {
        private readonly IMongoCollection<Sleeve> _sleeveCollection;

        public SleeveRepository(IMongoClient mongoClient, IOptions<MongoDbConfiguration> mongoDBConfig)
        {
            IMongoDatabase database = mongoClient.GetDatabase(mongoDBConfig.Value.DatabaseName);
            _sleeveCollection = database.GetCollection<Sleeve>(DeviceManagerConstants.Collections.SLEEVE_COLLECTION);
        }

        public async Task<Sleeve> CreateSleeveAsync(CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken = default)
        {
            int nextId = await GetNextSleeveIdAsync(cancellationToken);
            Sleeve sleeveEntity = new Sleeve(nextId, createSleeveDTO.Name, createSleeveDTO.Location, createSleeveDTO.PortNumbers);
            await _sleeveCollection.InsertOneAsync(sleeveEntity, null, cancellationToken);
            return sleeveEntity;
        }

        private async Task<int> GetNextSleeveIdAsync(CancellationToken cancellationToken)
        {
            List<Sleeve> sleeves = await _sleeveCollection.Find(FilterDefinition<Sleeve>.Empty)
                .SortByDescending(s => s.Id)
                .Limit(1)
                .ToListAsync(cancellationToken);
            Sleeve? maxSleeve = sleeves.FirstOrDefault();
            return (maxSleeve?.Id ?? 0) + 1;
        }

        public async Task<bool> DeleteSleeveAsync(string name, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.Name, name);
            DeleteResult result = await _sleeveCollection.DeleteOneAsync(filter, cancellationToken);
            return result.DeletedCount > 0;
        }

        public Task<bool> DoesSleeveExistAsync(string name, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.Name, name);
            return _sleeveCollection.Find(filter).AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<SleeveRo>> GetAllSleevesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Sleeve> sleeves = await _sleeveCollection.Find(FilterDefinition<Sleeve>.Empty).ToListAsync(cancellationToken);
            return sleeves.ToRo();
        }

        public async Task<SleeveRo> GetSleeveByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.Name, name);
            Sleeve sleeve = await _sleeveCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return sleeve.ToRo();
        }

        public async Task<bool> UpdateSleeveAsync(string name, UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.Name, name);
            List<UpdateDefinition<Sleeve>> updates = new List<UpdateDefinition<Sleeve>>();

            if (updateSleeveDTO.Name != null)
                updates.Add(Builders<Sleeve>.Update.Set(s => s.Name, updateSleeveDTO.Name));

            if (updateSleeveDTO.Location != null)
                updates.Add(Builders<Sleeve>.Update.Set(s => s.Location, updateSleeveDTO.Location));

            if (updateSleeveDTO.PortNumbers != null)
                updates.Add(Builders<Sleeve>.Update.Set(s => s.PortNumbers, updateSleeveDTO.PortNumbers));

            if (updates.Count == 0)
                return false;

            UpdateDefinition<Sleeve> combinedUpdate = Builders<Sleeve>.Update.Combine(updates);
            UpdateResult result = await _sleeveCollection.UpdateOneAsync(filter, combinedUpdate, null, cancellationToken);
            return result.ModifiedCount > 0;
        }

        public async Task<IEnumerable<int>> GetAvailableSleeveForUAVAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> existingFilter = Builders<Sleeve>.Filter.Eq(s => s.AssignedToTailId, tailId);
            Sleeve existingSleeve = await _sleeveCollection.Find(existingFilter).FirstOrDefaultAsync(cancellationToken);
            if (existingSleeve != null)
            {
                return existingSleeve.PortNumbers;
            }

            FilterDefinition<Sleeve> availableFilter = Builders<Sleeve>.Filter.Eq(s => s.AssignedToTailId, null);
            UpdateDefinition<Sleeve> update = Builders<Sleeve>.Update.Set(s => s.AssignedToTailId, tailId);
            FindOneAndUpdateOptions<Sleeve> options = new FindOneAndUpdateOptions<Sleeve>
            {
                ReturnDocument = ReturnDocument.After
            };
            Sleeve assignedSleeve = await _sleeveCollection.FindOneAndUpdateAsync(availableFilter, update, options, cancellationToken);
            return assignedSleeve?.PortNumbers ?? Enumerable.Empty<int>();
        }

        public async Task<bool> ReleaseSleeveByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.AssignedToTailId, tailId);
            UpdateDefinition<Sleeve> update = Builders<Sleeve>.Update.Set(s => s.AssignedToTailId, null);
            UpdateResult result = await _sleeveCollection.UpdateManyAsync(filter, update, null, cancellationToken);
            return result.ModifiedCount > 0;
        }

        public async Task ReassignSleeveAsync(int oldTailId, int newTailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> filter = Builders<Sleeve>.Filter.Eq(s => s.AssignedToTailId, oldTailId);
            UpdateDefinition<Sleeve> update = Builders<Sleeve>.Update.Set(s => s.AssignedToTailId, newTailId);
            await _sleeveCollection.UpdateManyAsync(filter, update, null, cancellationToken);
        }

        public async Task<bool> AssignSleeveToUavAsync(int tailId, string sleeveName, CancellationToken cancellationToken = default)
        {
            FilterDefinition<Sleeve> sleeveFilter = Builders<Sleeve>.Filter.Eq(s => s.Name, sleeveName);
            Sleeve? sleeve = await _sleeveCollection.Find(sleeveFilter).FirstOrDefaultAsync(cancellationToken);
            if (sleeve == null)
            {
                return false;
            }

            if (sleeve.AssignedToTailId == tailId)
            {
                return true;
            }

            await ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
            UpdateDefinition<Sleeve> assignUpdate = Builders<Sleeve>.Update.Set(s => s.AssignedToTailId, tailId);
            UpdateResult assignResult = await _sleeveCollection.UpdateOneAsync(sleeveFilter, assignUpdate, null, cancellationToken);
            return assignResult.ModifiedCount > 0;
        }
    }
}
