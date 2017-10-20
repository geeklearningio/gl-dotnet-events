namespace GeekLearning.Events.Internal
{
    using GeekLearning.Events.Configuration.Queue;
    using GeekLearning.Events.Configuration;
    using GeekLearning.Events.Configuration.Provider;
    using Microsoft.Extensions.Options;

    public abstract class EventsProviderBase<TParsedOptions, TInstanceOptions, TQueueOptions> : IEventProvider
        where TParsedOptions : class, IParsedOptions<TInstanceOptions, TQueueOptions>, new()
        where TInstanceOptions : class, IProviderInstanceOptions, new()
        where TQueueOptions : class, IQueueOptions, new()
    {
        protected readonly TParsedOptions options;

        public abstract string Name { get; }

        public EventsProviderBase(IOptions<TParsedOptions> options)
        {
            this.options = options.Value;
        }

        public IEventQueuer BuildQueueProvider(string queueName)
        {
            return this.BuildQueueInternal(queueName, this.options.GetQueueConfiguration(queueName));
        }

        public IEventQueuer BuildQueueProvider(string queueName, IQueueOptions queueOptions)
        {
            if(queueOptions.ProviderType != this.Name)
            {
                throw new Exceptions.BadQueueProvider(this.Name, queueName);
            }

            return this.BuildQueueInternal(queueName, queueOptions.ParseQueueOptions<TParsedOptions,TInstanceOptions,TQueueOptions>(options));
        }

        protected abstract IEventQueuer BuildQueueInternal(string queueName, TQueueOptions queueOptions);
    }
}
