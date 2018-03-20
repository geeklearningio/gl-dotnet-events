namespace GeekLearning.Events
{
    using GeekLearning.Events.Configuration.Queue;

    public interface IEventFactory
    {
        IEventQueuer GetQueuer(string queueName, IQueueOptions configuration);

        IEventQueuer GetQueuer(string queueName);

        bool TryGetQueuer(string queueName, out IEventQueuer queuer);

        bool TryGetQueuer(string queueName, out IEventQueuer queuer, string provider);
    }
}
