namespace GeekLearning.Events.Configuration
{
    using GeekLearning.Events.Configuration.Provider;
    using GeekLearning.Events.Configuration.Queue;
    using System.Collections.Generic;

    public interface IParsedOptions<TInstanceOptions, TQueueOptions>
        where TInstanceOptions : class, IProviderInstanceOptions
        where TQueueOptions : class, IQueueOptions
    {
        string Name { get; }

        IReadOnlyDictionary<string, string> ConnectionStrings { get; set; }

        IReadOnlyDictionary<string, TInstanceOptions> ParsedProviderInstances { get; set; }

        IReadOnlyDictionary<string, TQueueOptions> ParsedQueueOptions { get; set; }

        void BindProviderInstanceOptions(TInstanceOptions providerInstanceOptions);

        void BindQueueOptions(TQueueOptions queueOptions, TInstanceOptions providerInstanceOptions = null);
    }
}
