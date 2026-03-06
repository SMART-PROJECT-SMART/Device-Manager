namespace DeviceManager.Common.Constants
{
    public static class DeviceManagerConstants
    {
        public static class Configuration {
            public const string MONGODB_CONFIG_SECTION = "MongoDbConfiguration";
            public const string SIMULATOR_CONFIG_SECTION = "Simulator";
            public const string TELEMETRY_DEVICE_CONFIG_SECTION = "TelemetryDevice";
            public const string ACM_CONFIG_SECTION = "ACM";
            public const string MONGO_CONSUMER_CONFIG_SECTION = "MongoConsumer";
            public const string KAFKA_CONFIG_SECTION = "Kafka";
            public const string ICD_CONFIG_SECTION = "ICD";
        }
        public static class Collections {
            public const string UAV_COLLECTION = "UAVs";
            public const string SLEEVE_COLLECTION = "Sleeves";
        }
        public static class ErrorMessages {
            public const string UAV_NOT_FOUND = "UAV with TailId {0} not found.";
            public const string UAV_CREATE_FAILED = "Failed to create UAV.";
            public const string UAV_UPDATE_FAILED = "Failed to update UAV with TailId {0}.";
            public const string UAV_DELETE_FAILED = "Failed to delete UAV with TailId {0}.";
            public const string SLEEVE_NOT_FOUND = "Sleeve with name '{0}' not found.";
            public const string SLEEVE_ID_NOT_FOUND = "Sleeve with id '{0}' not found.";
            public const string SLEEVE_CREATE_FAILED = "Failed to create Sleeve.";
            public const string SLEEVE_UPDATE_FAILED = "Failed to update Sleeve with name '{0}'.";
            public const string SLEEVE_DELETE_FAILED = "Failed to delete Sleeve with name '{0}'.";
            public const string NO_AVAILABLE_SLEEVE = "No available Sleeve found for UAV with TailId {0}.";
            public const string SLEEVE_RELEASE_FAILED = "Failed to release Sleeve for UAV with TailId {0}.";
            public const string INVALID_MODEL_STATE = "The request contains invalid data.";
        }
    }
}
