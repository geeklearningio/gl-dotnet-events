namespace GeekLearning.Events.AzureStorage.Configuration
{
    using System.Collections.Generic;
    using GeekLearning.Events.Configuration;

    public class AzureStorageParsedOptions : IParsedOptions<AzureStorageProviderInstanceOptions, AzureStorageQueueOptions>
    {
        public string Name => AzureStorageProvider.ProviderName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public IReadOnlyDictionary<string, AzureStorageProviderInstanceOptions> ParsedProviderInstances { get; set; }

        public IReadOnlyDictionary<string, AzureStorageQueueOptions> ParsedQueueOptions { get; set; }

        public void BindProviderInstanceOptions(AzureStorageProviderInstanceOptions providerInstanceOptions)
        {
            if (!string.IsNullOrEmpty(providerInstanceOptions.ConnectionStringName)
                && string.IsNullOrEmpty(providerInstanceOptions.ConnectionString))
            {
                if (!this.ConnectionStrings.ContainsKey(providerInstanceOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadProviderConfiguration(
                        providerInstanceOptions.Name,
                        $"The ConnectionString named '{providerInstanceOptions.Name}' cannot be found. Didi you call AddEvent with the configuration root ? ");
                }

                providerInstanceOptions.ConnectionString = this.ConnectionStrings[providerInstanceOptions.ConnectionStringName];
            }
        }

        public void BindQueueOptions(AzureStorageQueueOptions queueOptions, AzureStorageProviderInstanceOptions providerInstanceOptions = null)
        {
            if (!string.IsNullOrEmpty(queueOptions.ConnectionStringName)
                && string.IsNullOrEmpty(queueOptions.ConnectionStringName))
            {
                if (!this.ConnectionStrings.ContainsKey(queueOptions.ConnectionStringName))
                {
                    throw new Exceptions.BadQueueConfiguration(
                        queueOptions.Name,
                        $"The ConnectionString named '{queueOptions.ConnectionStringName}' cannot be found. Did you call AddEvent with the configuration root ?");
                }

                queueOptions.ConnectionString = this.ConnectionStrings[queueOptions.ConnectionStringName];
            }

            if (providerInstanceOptions == null || queueOptions.ProviderName != providerInstanceOptions.Name)
            {
                return;
            }

            if (string.IsNullOrEmpty(queueOptions.ConnectionString))
            {
                queueOptions.ConnectionString = providerInstanceOptions.ConnectionString;
            }
        }
    }
}
