using Core.Models;

namespace DeviceManager.Models.Dto
{
    public class UpdateUAVDto
    {
        public int? TailId { get; set; }
        public Location? BaseLocation { get; set; }
    }
}
