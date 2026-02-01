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
        public PlatformType platformType { get; set; }
        [Required]
        public Location BaseLocation { get; set; }

    }
}
