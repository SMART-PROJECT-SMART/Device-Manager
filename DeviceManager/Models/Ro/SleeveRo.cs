using Core.Models;

namespace DeviceManager.Models.Ro
{
    public class SleeveRo
    {
        public SleeveRo(int id, string name, Location location, IEnumerable<int> portNumbers, int? assignedToTailId = null)
        {
            Id = id;
            Name = name;
            Location = location;
            PortNumbers = portNumbers;
            AssignedToTailId = assignedToTailId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public IEnumerable<int> PortNumbers { get; set; }
        public int? AssignedToTailId { get; set; }
    }
}
