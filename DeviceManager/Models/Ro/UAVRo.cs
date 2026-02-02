using Core.Common.Enums;
using Core.Models;

namespace DeviceManager.Models.Ro
{
    public class UAVRo
    {
        public UAVRo(int tailId, PlatformType platformType, Location baseLocation)
        {
            TailId = tailId;
            PlatformType = platformType;
            BaseLocation = baseLocation;
        }

        public int TailId { get; set; }
        public PlatformType PlatformType { get; set; }
        public Location BaseLocation { get; set; }
    }
}
