using DeviceManager.Common.Constants;
using DeviceManager.Config;
using MongoDB.Driver;
using System.Text.Json.Serialization;

namespace DeviceManager.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services) {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddEndpointsApiExplorer();
            return services;
        }
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<MongoDbConfiguration>(configuration.GetSection(DeviceManagerConstants.Configuration.MONGODB_CONFIG_SECTION));
            services.AddSingleton<IMongoClient>(sp =>
            {
                MongoDbConfiguration config = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<MongoDbConfiguration>>().Value;
                return new MongoClient(config.ConnectionString);
            });
            return services;
        }
    }
}
