using Core.Common.Enums;
using Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManager.Database.MongoDB.Entities
{
    public class UAV
    {
        public UAV(int tailId, PlatformType platformType, Location baseLocation)
        {
            TailId = tailId;
            PlatformType = platformType;
            BaseLocation = baseLocation;
        }

        [BsonRequired]
        [BsonId]
        public int TailId { get; set; }
        [BsonRequired]
        public PlatformType PlatformType { get; set; }
        [BsonRequired]
        public Location BaseLocation { get; set; }
    }
}
