namespace GeekLearning.Events.AzureStorage
{
    using GeekLearning.Events.AzureStorage.Configuration;
    using GeekLearning.Events.Internal;
    using Microsoft.Extensions.Options;

    public class AzureStorageProvider : EventsProviderBase<AzureStorageParsedOptions, AzureStorageProviderInstanceOptions, AzureStorageQueueOptions>
    {
        public const string ProviderName = "AzureStorage";

        public override string Name => ProviderName;

        public AzureStorageProvider(IOptions<AzureStorageParsedOptions> options)
            : base(options)
        {
        }

        protected override InMemory BuildQueueInternal(string queueName, AzureStorageQueueOptions queueOptions)
        {
            return new AzureStorageEventQueuer(queueOptions);
        }
    }
}
