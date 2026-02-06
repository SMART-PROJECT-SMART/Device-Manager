namespace DeviceManager.Models.Config
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public MongoDbConfiguration() { }
        public MongoDbConfiguration(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
    }
}
