using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Core.Configuration;
using Core.Services;
using DeviceManager.Common.Constants;
using DeviceManager.Models.Config;
using DeviceManager.Repositories.SleeveRepository.Interfaces;
using DeviceManager.Repositories.UAVRepository;
using DeviceManager.Repositories.UAVRepository.Interfaces;
using DeviceManager.Services.Kafka;
using DeviceManager.Services.MongoDB.SleeveDBService;
using DeviceManager.Services.SimulatorNotification;
using DeviceManager.Services.SimulatorNotification.Interfaces;
using DeviceManager.Services.SleeveRepository;
using DeviceManager.Services.SleeveService.Interfaces;
using DeviceManager.Services.TelemetryDeviceNotification;
using DeviceManager.Services.TelemetryDeviceNotification.Interfaces;
using DeviceManager.Services.UAVDBService;
using DeviceManager.Services.UAVDBService.Interfaces;
using MongoDB.Driver;
using System.Text.Json.Serialization;

namespace DeviceManager.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<MongoDbConfiguration>(configuration.GetSection(DeviceManagerConstants.Configuration.MONGODB_CONFIG_SECTION));
            services.Configure<SimulatorConfiguration>(configuration.GetSection(DeviceManagerConstants.Configuration.SIMULATOR_CONFIG_SECTION));
            services.Configure<TelemetryDeviceConfiguration>(configuration.GetSection(DeviceManagerConstants.Configuration.TELEMETRY_DEVICE_CONFIG_SECTION));
            services.Configure<KafkaConfiguration>(configuration.GetSection(DeviceManagerConstants.Configuration.KAFKA_CONFIG_SECTION));
            services.Configure<ICDSettings>(configuration.GetSection(DeviceManagerConstants.Configuration.ICD_CONFIG_SECTION));
            services.AddSingleton<IMongoClient>(sp =>
            {
                MongoDbConfiguration config = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<MongoDbConfiguration>>().Value;
                return new MongoClient(config.ConnectionString);
            });
            return services;
        }
        public static IServiceCollection AddMongoDBServices(this IServiceCollection services) {
            services.AddScoped<IUAVRepository, UAVRepository>();
            services.AddScoped<ISleeveRepository, SleeveRepository>();
            services.AddScoped<IUAVService, UAVService>();
            services.AddScoped<ISleeveService, SleeveService>();
            return services;
        }

        public static IServiceCollection AddSimulatorNotification(this IServiceCollection services) {
            services.AddHttpClient<ISimulatorNotificationService, SimulatorNotificationService>();
            return services;
        }

        public static IServiceCollection AddTelemetryDeviceNotification(this IServiceCollection services) {
            services.AddHttpClient<ITelemetryDeviceNotificationService, TelemetryDeviceNotificationService>();
            return services;
        }

        public static IServiceCollection AddKafkaServices(this IServiceCollection services) {
            services.AddSingleton<IAdminClient>(provider =>
            {
                var kafkaConfig = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<KafkaConfiguration>>().Value;
                var adminConfig = new AdminClientConfig
                {
                    BootstrapServers = kafkaConfig.BootstrapServers
                };
                return new AdminClientBuilder(adminConfig).Build();
            });

            services.AddIcdDirectory();
            services.AddSingleton<IKafkaTopicManager, KafkaTopicManager>();
            return services;
        }
    }
}
