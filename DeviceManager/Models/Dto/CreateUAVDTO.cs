using Core.Common.Enums;
using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Models.Dto
{
    public class CreateUAVDTO
    {
        [Required]
        public int TailId { get; set; }
        [Required]
        public PlatformType PlatformType { get; set; }
        [Required]
        public Location BaseLocation { get; set; }
    }
}
