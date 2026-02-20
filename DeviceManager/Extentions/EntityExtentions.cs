using Core.Common.Helpers;
using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Extentions
{
    public static class EntityExtentions
    {
        public static UAV ToEntity(this CreateUAVDTO dto) {
            var baseLocation = BaseLocationHelper.GetBaseLocation(dto.BaseLocation);
            return new UAV(dto.TailId, dto.PlatformType, baseLocation);
        }
        public static UAVRo ToRo(this UAV uav) {
            if (uav == null) return null;
            var location = BaseLocationHelper.GetLocation(uav.BaseLocation);
            return new UAVRo(uav.TailId, uav.PlatformType, location);
        }
        public static IEnumerable<UAVRo> ToRo(this IEnumerable<UAV> uav) {
            return uav.Select(u => u.ToRo());
        }


        public static SleeveRo ToRo(this Sleeve sleeve)
        {
            if (sleeve == null) return null;
            return new SleeveRo(sleeve.Id, sleeve.Name, sleeve.Location, sleeve.PortNumbers, sleeve.AssignedToTailId);
        }

        public static IEnumerable<SleeveRo> ToRo(this IEnumerable<Sleeve> sleeves)
        {
            return sleeves.Select(s => s.ToRo());
        }
    }
}
