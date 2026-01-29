using DeviceManager.Common.Constants;
using DeviceManager.Config;
using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Database.MongoDB.Repositories.Interfaces;
using DeviceManager.Extentions;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeviceManager.Database.MongoDB.Repositories
{
    public class UAVRepository : IUAVRepository
    {
        private readonly IMongoCollection<UAV> _uavCollection;
        public UAVRepository(IMongoClient mongoClient, IOptions<MongoDbConfiguration> mongoDBConfig) {
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

        public Task<bool> DoesUAVExists(int tailId)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            return _uavCollection.Find(filter).AnyAsync();
        }

        public async Task<IEnumerable<UAVRo>> GetAllUAVsAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<UAV> uavs = await _uavCollection.Find(_ => true).ToListAsync();
            return uavs.ToRo();
        }

        public Task<UAVRo> GetUAVByTailIdAsync(int tailId, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            return _uavCollection.Find(filter).FirstOrDefaultAsync().ContinueWith(task => task.Result.ToRo(), cancellationToken);
        }

        public Task<bool> UpdateUAVAsync(int tailId, UpdateUAVDto updateUAVDto, CancellationToken cancellationToken = default)
        {
            FilterDefinition<UAV> filter = Builders<UAV>.Filter.Eq(u => u.TailId, tailId);
            var updateBuilder = Builders<UAV>.Update;
            UpdateDefinition<UAV> update = null;
            if (updateUAVDto.TailId.HasValue)
            {
                update = (update == null)
                    ? updateBuilder.Set(u => u.TailId, updateUAVDto.TailId.Value)
                    : update.Set(u => u.TailId, updateUAVDto.TailId.Value);
            }
            if (updateUAVDto.BaseLocation != null)
            {
                update = (update == null)
                    ? updateBuilder.Set(u => u.BaseLocation, updateUAVDto.BaseLocation)
                    : update.Set(u => u.BaseLocation, updateUAVDto.BaseLocation);
            }
            return _uavCollection.UpdateOneAsync(filter, update, null, cancellationToken)
                .ContinueWith(task => task.Result.ModifiedCount > 0, cancellationToken);
        }
    }
}
