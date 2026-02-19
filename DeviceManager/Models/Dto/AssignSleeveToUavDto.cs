using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Models.Dto
{
    public class AssignSleeveToUavDto
    {
        [Required]
        public int TailId { get; set; }

        [Required]
        public string SleeveName { get; set; } = string.Empty;
    }
}
