namespace DeviceManager.Common.Constants
{
    public static class DeviceManagerConstants
    {
        public static class Configuration {
            public const string MONGODB_CONFIG_SECTION = "MongoDbConfiguration";
            public const string SIMULATOR_CONFIG_SECTION = "Simulator";
            public const string TELEMETRY_DEVICE_CONFIG_SECTION = "TelemetryDevice";
        }
        public static class Collections {
            public const string UAV_COLLECTION = "UAVs";
            public const string SLEEVE_COLLECTION = "Sleeves";
        }
    }
}
