namespace DeviceManager.Models.Config
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string TopicPrefix { get; set; } = string.Empty;
        public short ReplicationFactor { get; set; } = 1;
        public KafkaConfiguration() { }
        public KafkaConfiguration(string bootstrapServers, string topicPrefix, short replicationFactor = 1)
        {
            BootstrapServers = bootstrapServers;
            TopicPrefix = topicPrefix;
            ReplicationFactor = replicationFactor;
        }
    }
}
