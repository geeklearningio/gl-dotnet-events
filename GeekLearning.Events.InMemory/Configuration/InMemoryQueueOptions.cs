namespace GeekLearning.Events.InMemory.Configuration
{
    using GeekLearning.Events.Configuration.Queue;

    public class InMemoryQueueOptions : QueueOptions
    {
        public string QueueName { get; set; }
    }
}
