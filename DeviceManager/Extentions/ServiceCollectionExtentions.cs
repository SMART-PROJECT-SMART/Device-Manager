using DeviceManager.Common.Constants;
using DeviceManager.Config;
using DeviceManager.Database.MongoDB.Repositories.UAVRepository;
using DeviceManager.Database.MongoDB.Repositories.SleeveRepository;
using DeviceManager.Database.MongoDB.Services.UAVDBService;
using DeviceManager.Database.MongoDB.Services.UAVDBService.Interfaces;
using DeviceManager.Database.MongoDB.Services.SleeveDBService;
using DeviceManager.Services.SimulatorNotification;
using DeviceManager.Services.TelemetryDeviceNotification;
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
            services.AddSingleton<IMongoClient>(sp =>
            {
                MongoDbConfiguration config = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<MongoDbConfiguration>>().Value;
                return new MongoClient(config.ConnectionString);
            });
            return services;
        }
        public static IServiceCollection AddMongoDBServices(this IServiceCollection services) {
            services.AddSingleton<IUAVRepository, UAVRepository>();
            services.AddSingleton<ISleeveRepository, SleeveRepository>();
            services.AddSingleton<IUAVDBService, UAVDBService>();
            services.AddSingleton<ISleeveDBService, SleeveDBService>();
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
    }
}
