using DeviceManager.Common.Constants;
using DeviceManager.Config;
using DeviceManager.Services.SimulatorNotification;
using DeviceManager.Services.TelemetryDeviceNotification;
using MongoDB.Driver;
using System.Text.Json.Serialization;
using DeviceManager.Services.MongoDB.SleeveDBService;
using DeviceManager.Services.SleeveRepository;
using DeviceManager.Services.UAVDBService;
using DeviceManager.Services.UAVDBService.Interfaces;
using DeviceManager.Repositories.UAVRepository;
using DeviceManager.Repositories.UAVRepository.Interfaces;
using DeviceManager.Services.SimulatorNotification.Interfaces;
using DeviceManager.Services.SleeveDBService.Interfaces;
using DeviceManager.Repositories.SleeveRepository.Interfaces;
using DeviceManager.Services.TelemetryDeviceNotification.Interfaces;

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
    }
}
