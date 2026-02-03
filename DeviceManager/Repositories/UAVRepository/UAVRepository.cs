using DeviceManager.Common.Constants;
using DeviceManager.Extentions;
using DeviceManager.Models;
using DeviceManager.Models.Config;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Repositories.UAVRepository.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeviceManager.Repositories.UAVRepository
{
    public class UAVRepository : IUAVRepository
    {
        private readonly IMongoCollection<UAV> _uavCollection;

        public UAVRepository(IMongoClient mongoClient, IOptions<MongoDbConfiguration> mongoDBConfig)
        {
            IMongoDatabase database = mongoClient.GetDatabase(mongoDBConfig.Value.DatabaseName);
            _uavCollection = database.GetCollection<UAV>(DeviceManagerConstants.Collections.UAV_COLLECTION);
        }

        public async Task<UAV> CreateUAVAsync(CreateUAVDTO createUAVDTO, CancellationToken cancellationToken = default)
        {
            UAV uavEntity = createUAVDTO.ToEntity();
            await _uavCollection.InsertOneAsync(uavEntity, null, cancellationToken);
            return uavEntity;
        }

        public async Task<bool> DeleteUAVAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            DeleteResult result = await _uavCollection.DeleteOneAsync(filter, cancellationToken);
            return result.DeletedCount > 0;
        }

        public Task<bool> DoesUAVExistAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            return _uavCollection.Find(filter).AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<UAV> uavs = await _uavCollection.Find(FilterDefinition<UAV>.Empty).ToListAsync(cancellationToken);
            return uavs.ToRo();
        }

        public async Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            UAV uav = await _uavCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return uav.ToRo();
        }

        public async Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            List<UpdateDefinition<UAV>> updates = new List<UpdateDefinition<UAV>>();

            if (updateUAVDto.TailId.HasValue)
                updates.Add(Builders<UAV>.Update.Set(u => u.TailId, updateUAVDto.TailId.Value));

            if (updateUAVDto.BaseLocation != null)
                updates.Add(Builders<UAV>.Update.Set(u => u.BaseLocation, updateUAVDto.BaseLocation));

            if (updates.Count == 0)
                return false;

            UpdateDefinition<UAV> combinedUpdate = Builders<UAV>.Update.Combine(updates);
            UpdateResult result = await _uavCollection.UpdateOneAsync(filter, combinedUpdate, null, cancellationToken);
            return result.ModifiedCount > 0;
        }
    }
}
