using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Models.Dto
{
    public class AssignSleeveToUavDto
    {
        [Required]
        public int TailId { get; set; }

        [Required]
        public int SleeveId { get; set; }
    }
}
