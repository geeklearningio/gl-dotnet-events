namespace GeekLearning.Events.InMemory
{
    using GeekLearning.Events.InMemory.Configuration;
    using GeekLearning.Events.InMemory.Internal;
    using GeekLearning.Events.Internal;
    using Microsoft.Extensions.Options;

    public class InMemoryProvider : EventsProviderBase<InMemoryParsedOptions, InMemoryProviderInstanceOptions, InMemoryQueueOptions>
    {
        public const string ProviderName = "InMemory";

        public override string Name => ProviderName;

        private IQueueStorageInMemory storedQueue;

        public InMemoryProvider(IQueueStorageInMemory storedQueue, IOptions<InMemoryParsedOptions> options)
            : base(options)
        {
            this.storedQueue = storedQueue;
        }

        protected override IEventQueuer BuildQueueInternal(string queueName, InMemoryQueueOptions queueOptions)
        {
            return new InMemoryEventQueuer(this.storedQueue, queueOptions);
        }
    }
}
