using Core.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManager.Database.MongoDB.Entities
{
    public class Sleeve
    {
        public Sleeve(string name, Location location, IEnumerable<int> portNumbers)
        {
            Name = name;
            Location = location;
            PortNumbers = portNumbers;
            AssignedToTailId = null;
        }

        [BsonRequired]
        [BsonId]
        public string Name { get; set; }
        [BsonRequired]
        public Location Location { get; set; }
        [BsonRequired]
        public IEnumerable<int> PortNumbers { get; set; }
        public int? AssignedToTailId { get; set; }
    }
}
