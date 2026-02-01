using Core.Common.Enums;

namespace DeviceManager.Models.Dto
{
    public class UAVChangedNotificationDTO
    {
        public UAVChangedNotificationDTO(CrudOperation operation, int tailId)
        {
            Operation = operation;
            TailId = tailId;
        }

        public CrudOperation Operation { get; set; }
        public int TailId { get; set; }
    }
}
