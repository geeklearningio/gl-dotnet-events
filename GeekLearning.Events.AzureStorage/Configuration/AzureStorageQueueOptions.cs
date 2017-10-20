namespace GeekLearning.Events.AzureStorage.Configuration
{
    using GeekLearning.Events.Configuration.Queue;

    public class AzureStorageQueueOptions : QueueOptions
    {
        public string ConnectionString { get; set; }

        public string ConnectionStringName { get; set; }

    }
}
