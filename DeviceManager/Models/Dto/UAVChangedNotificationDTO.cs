using Core.Common.Enums;

namespace DeviceManager.Models.Dto
{
    public class UAVChangedNotificationDTO
    {
        public UAVChangedNotificationDTO(CrudOperation operation, int tailId, int? newTailId = null)
        {
            Operation = operation;
            TailId = tailId;
            NewTailId = newTailId;
        }

        public CrudOperation Operation { get; set; }
        public int TailId { get; set; }
        public int? NewTailId { get; set; }
    }
}
