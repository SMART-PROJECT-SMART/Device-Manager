using Core.Common.Enums;

namespace DeviceManager.Models.Dto
{
    public class SleeveChangedNotificationDTO
    {
        public SleeveChangedNotificationDTO(CrudOperation operation, string name)
        {
            Operation = operation;
            Name = name;
        }

        public CrudOperation Operation { get; set; }
        public string Name { get; set; }
    }
}
