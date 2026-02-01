using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Models.Dto
{
    public class CreateSleeveDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public IEnumerable<int> PortNumbers { get; set; }
    }
}
