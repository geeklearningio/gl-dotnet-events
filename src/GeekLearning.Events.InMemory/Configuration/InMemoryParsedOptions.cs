namespace GeekLearning.Events.InMemory.Configuration
{
    using System.Collections.Generic;
    using GeekLearning.Events.Configuration;

    public class InMemoryParsedOptions : IParsedOptions<InMemoryProviderInstanceOptions, InMemoryQueueOptions>
    {
        public string Name => InMemoryProvider.ProviderName;

        public IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        public IReadOnlyDictionary<string, InMemoryProviderInstanceOptions> ParsedProviderInstances { get; set; }

        public IReadOnlyDictionary<string, InMemoryQueueOptions> ParsedQueueOptions { get; set; }

        public void BindProviderInstanceOptions(InMemoryProviderInstanceOptions providerInstanceOptions)
        {
        }

        public void BindQueueOptions(InMemoryQueueOptions queueOptions, InMemoryProviderInstanceOptions providerInstanceOptions = null)
        {
        }
    }
}
