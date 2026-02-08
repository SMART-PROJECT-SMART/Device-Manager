namespace DeviceManager.Services.Kafka
{
    public interface IKafkaTopicManager
    {
        Task CreateTopicAsync(int tailId, CancellationToken cancellationToken = default);
        Task DeleteTopicAsync(int tailId, CancellationToken cancellationToken = default);
        Task UpdateTopicAsync(int tailId, CancellationToken cancellationToken = default);
    }
}
