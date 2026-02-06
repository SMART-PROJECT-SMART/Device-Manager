using DeviceManager.Models;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Extentions
{
    public static class EntityExtentions
    {
        public static UAV ToEntity(this CreateUAVDTO dto) {
            return new UAV(dto.TailId, dto.PlatformType, dto.BaseLocation);
        }
        public static UAVRo ToRo(this UAV uav) {
            if (uav == null) return null;
            return new UAVRo(uav.TailId, uav.PlatformType, uav.BaseLocation);
        }
        public static IEnumerable<UAVRo> ToRo(this IEnumerable<UAV> uav) {
            return uav.Select(u => u.ToRo());
        }

        public static Sleeve ToEntity(this CreateSleeveDTO dto)
        {
            return new Sleeve(dto.Name, dto.Location, dto.PortNumbers);
        }

        public static SleeveRo ToRo(this Sleeve sleeve)
        {
            if (sleeve == null) return null;
            return new SleeveRo(sleeve.Name, sleeve.Location, sleeve.PortNumbers, sleeve.AssignedToTailId);
        }

        public static IEnumerable<SleeveRo> ToRo(this IEnumerable<Sleeve> sleeves)
        {
            return sleeves.Select(s => s.ToRo());
        }
    }
}
