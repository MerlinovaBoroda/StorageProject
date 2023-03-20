namespace StorageProject.Api.Configurations
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ItemsCollection { get; set; } = null!;
    }
}
