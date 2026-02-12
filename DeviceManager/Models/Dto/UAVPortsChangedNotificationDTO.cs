namespace DeviceManager.Models.Dto
{
    public class UAVPortsChangedNotificationDTO
    {
        public UAVPortsChangedNotificationDTO(int tailId, IEnumerable<int> newPorts)
        {
            TailId = tailId;
            NewPorts = newPorts;
        }

        public int TailId { get; set; }
        public IEnumerable<int> NewPorts { get; set; }
    }
}
