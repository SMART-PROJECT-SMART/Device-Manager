using Core.Models;

namespace DeviceManager.Models.Dto
{
    public class UpdateSleeveDTO
    {
        public string? Name { get; set; }
        public Location? Location { get; set; }
        public IEnumerable<int>? PortNumbers { get; set; }
    }
}
