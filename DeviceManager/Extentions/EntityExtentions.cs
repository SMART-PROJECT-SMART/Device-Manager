using DeviceManager.Database.MongoDB.Entities;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using SharpCompress.Common.Tar;

namespace DeviceManager.Extentions
{
    public static class EntityExtentions
    {
        public static UAV ToEntity(this CreateUAVDTO dto) {
            return new UAV(dto.TailId, dto.platformType, dto.BaseLocation);
        }
        public static UAVRo ToRo(this UAV uav) {
            return new UAVRo(uav.TailId, uav.PlatformType, uav.BaseLocation);
        }
        public static IEnumerable<UAVRo> ToRo(this IEnumerable<UAV> uav) {
            return uav.Select(u => u.ToRo());
        }
    }
}
