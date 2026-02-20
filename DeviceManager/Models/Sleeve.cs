using Core.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace DeviceManager.Models
{
    public class Sleeve
    {
        public Sleeve(int id, string name, Location location, IEnumerable<int> portNumbers)
        {
            Id = id;
            Name = name;
            Location = location;
            PortNumbers = portNumbers;
            AssignedToTailId = null;
        }

        [BsonId]
        public int Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public Location Location { get; set; }
        [BsonRequired]
        public IEnumerable<int> PortNumbers { get; set; }
        public int? AssignedToTailId { get; set; }
    }
}
