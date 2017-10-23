namespace GeekLearning.Events.Internal
{
    using GeekLearning.Events.Configuration;
    using GeekLearning.Events.Configuration.Provider;
    using GeekLearning.Events.Configuration.Queue;
    using Microsoft.Extensions.Options;
    using System.Linq;

    public class ConfigureProviderOptions<TParsedOptions, TInstanceOptions, TQueueOptions> : IConfigureOptions<TParsedOptions>
        where TParsedOptions : class, IParsedOptions<TInstanceOptions, TQueueOptions>
        where TInstanceOptions : class, IProviderInstanceOptions, new()
        where TQueueOptions : class, IQueueOptions, new()

    {
        private readonly EventOptions eventOptions;

        public ConfigureProviderOptions(IOptions<EventOptions> eventOptions)
        {
            this.eventOptions = eventOptions.Value;
        }

        public void Configure(TParsedOptions options)
        {
            if (this.eventOptions == null)
            {
                return;
            }

            options.ConnectionStrings = this.eventOptions.ConnectionStrings;

            options.ParsedProviderInstances = this.eventOptions.Providers.Parse<TInstanceOptions>()
                .Where(kvp => kvp.Value.Type == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var parsedProviderInstance in options.ParsedProviderInstances)
            {
                parsedProviderInstance.Value.Compute<TParsedOptions, TInstanceOptions, TQueueOptions>(options);
            }

            var parsedQueues = this.eventOptions.Queues.Parse<TQueueOptions>();

            foreach (var parsedQueue in parsedQueues)
            {
                parsedQueue.Value.Compute<TParsedOptions, TInstanceOptions, TQueueOptions>(options);
            }

            options.ParsedQueueOptions = parsedQueues
                .Where(kvp => kvp.Value.ProviderType == options.Name)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
