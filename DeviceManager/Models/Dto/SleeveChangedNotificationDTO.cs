using Core.Common.Enums;

namespace DeviceManager.Models.Dto
{
    public class SleeveChangedNotificationDTO
    {
        public SleeveChangedNotificationDTO(CrudOperation operation, int id, string name)
        {
            Operation = operation;
            Id = id;
            Name = name;
        }

        public CrudOperation Operation { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
