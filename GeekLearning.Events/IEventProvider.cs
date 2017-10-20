namespace GeekLearning.Events
{
    using Configuration;
    using GeekLearning.Events.Configuration.Queue;

    public interface IEventProvider
    {
        string Name { get; }

        IEventQueuer BuildQueueProvider(string queueName, IQueueOptions storeOptions);

        IEventQueuer BuildQueueProvider(string queueName);
    }
}
