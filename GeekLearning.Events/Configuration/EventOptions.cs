namespace GeekLearning.Events.Configuration
{
    using GeekLearning.Events.Configuration.Provider;
    using GeekLearning.Events.Configuration.Queue;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;

    public class EventOptions : IParsedOptions<ProviderInstanceOptions, QueueOptions>
    {
        public const string DefaultConfigurationSectionName = "Event";

        private readonly Lazy<IReadOnlyDictionary<string, ProviderInstanceOptions>> parsedProviderInstances;
        private readonly Lazy<IReadOnlyDictionary<string, QueueOptions>> parsedQueueOptions;

        public EventOptions()
        {
            this.parsedProviderInstances = new Lazy<IReadOnlyDictionary<string, ProviderInstanceOptions>>(
                () => this.Providers.Parse<ProviderInstanceOptions>());
            this.parsedQueueOptions = new Lazy<IReadOnlyDictionary<string, QueueOptions>>(
                () => this.Queues.Parse<QueueOptions>());
        }

        public IReadOnlyDictionary<string, IConfigurationSection> Providers { get; set; }
        public IReadOnlyDictionary<string, IConfigurationSection> Queues { get; set; }

        public IReadOnlyDictionary<string, QueueOptions> ParsedQueueOptions { get => this.parsedQueueOptions.Value; set { } }
        public IReadOnlyDictionary<string, ProviderInstanceOptions> ParsedProviderInstances { get => this.parsedProviderInstances.Value; set { } }

        public string Name => DefaultConfigurationSectionName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public void BindQueueOptions(QueueOptions queueOptions, ProviderInstanceOptions providerInstanceOptions)
        {
        }

        public void BindProviderInstanceOptions(ProviderInstanceOptions providerInstanceOptions)
        {
        }
    }
}
