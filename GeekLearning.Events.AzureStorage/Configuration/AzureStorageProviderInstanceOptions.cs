namespace GeekLearning.Events.AzureStorage.Configuration
{
    using GeekLearning.Events.Configuration.Provider;

    public class AzureStorageProviderInstanceOptions : ProviderInstanceOptions
    {
        public string ConnectionString { get; set; }

        public string ConnectionStringName { get; set; }
    }
}
