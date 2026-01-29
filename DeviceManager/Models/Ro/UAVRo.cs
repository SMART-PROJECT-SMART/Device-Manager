using Core.Common.Enums;
using Core.Models;

namespace DeviceManager.Models.Ro
{
    public class UAVRo
    {
        private Location baseLocation;

        public UAVRo(int tailId, PlatformType platformType, Location baseLocation)
        {
            TailId = tailId;
            PlatformType = platformType;
            this.baseLocation = baseLocation;
        }

        public int TailId { get; set; }
        public PlatformType PlatformType { get; set; }

    }
}
