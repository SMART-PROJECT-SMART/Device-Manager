using Core.Common.Enums;
using Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManager.Models
{
    public class UAV
    {
        public UAV(int tailId, PlatformType platformType, BaseLocation baseLocation)
        {
            TailId = tailId;
            PlatformType = platformType;
            BaseLocation = baseLocation;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public int TailId { get; set; }
        [BsonRequired]
        public PlatformType PlatformType { get; set; }
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public BaseLocation BaseLocation { get; set; }
    }
}
