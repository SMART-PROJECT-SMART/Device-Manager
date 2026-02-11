using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Core.Services.ICDsDirectory;
using DeviceManager.Models.Config;
using Microsoft.Extensions.Options;

namespace DeviceManager.Services.Kafka
{
    public class KafkaTopicManager : IKafkaTopicManager
    {
        private readonly IAdminClient _adminClient;
        private readonly IICDDirectory _icdDirectory;
        private readonly KafkaConfiguration _kafkaConfig;

        public KafkaTopicManager(
            IAdminClient adminClient,
            IICDDirectory icdDirectory,
            IOptions<KafkaConfiguration> kafkaConfig)
        {
            _adminClient = adminClient;
            _icdDirectory = icdDirectory;
            _kafkaConfig = kafkaConfig.Value;
        }

        public async Task CreateTopicAsync(int tailId, CancellationToken cancellationToken = default)
        {
            try
            {
                string topicName = BuildTopicName(tailId);
                TopicSpecification topicSpec = BuildTopicSpecification(topicName);

                await _adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpec });
            }
            catch (CreateTopicsException ex)
                when (ex.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
            {
            }
            catch
            {
            }
        }

        public async Task DeleteTopicAsync(int tailId, CancellationToken cancellationToken = default)
        {
            try
            {
                string topicName = BuildTopicName(tailId);
                await _adminClient.DeleteTopicsAsync(new List<string> { topicName });
            }
            catch (DeleteTopicsException ex)
                when (ex.Results[0].Error.Code == ErrorCode.UnknownTopicOrPart)
            {
            }
            catch
            {
            }
        }

        public async Task UpdateTopicAsync(int tailId, int? newTailId = null, CancellationToken cancellationToken = default)
        {
            if (!newTailId.HasValue || newTailId.Value == tailId)
            {
                return;
            }

            await DeleteTopicAsync(tailId, cancellationToken);
            await CreateTopicAsync(newTailId.Value, cancellationToken);
        }

        private string BuildTopicName(int tailId)
        {
            return $"{_kafkaConfig.TopicPrefix}{tailId}";
        }

        private TopicSpecification BuildTopicSpecification(string topicName)
        {
            int partitionCount = _icdDirectory.GetICDCount();
            return new TopicSpecification
            {
                Name = topicName,
                NumPartitions = partitionCount,
                ReplicationFactor = _kafkaConfig.ReplicationFactor,
            };
        }
    }
}
