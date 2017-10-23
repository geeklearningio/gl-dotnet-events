namespace GeekLearning.Events.Configuration
{
    using GeekLearning.Events.Configuration.Provider;
    using GeekLearning.Events.Configuration.Queue;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    public static class ConfigurationExtensions
    {
        public static IReadOnlyDictionary<string, TOptions> Parse<TOptions>(this IReadOnlyDictionary<string, IConfigurationSection> unparsedConfiguration)
            where TOptions : class, INamedElementOptions, new()
        {
            if (unparsedConfiguration == null)
            {
                return new Dictionary<string, TOptions>();
            }

            return unparsedConfiguration
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => BindOptions<TOptions>(kvp));
        }

        public static void Compute<TParsedOptions, TInstanceOptions, TQueueOptions>(this TInstanceOptions parsedProviderInstance, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TInstanceOptions, TQueueOptions>
            where TInstanceOptions : class, IProviderInstanceOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            options.BindProviderInstanceOptions(parsedProviderInstance);
        }

        public static void Compute<TParsedOptions, TInstanceOptions, TQueueOptions>(this TQueueOptions parsedQueue, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TInstanceOptions, TQueueOptions>
            where TInstanceOptions : class, IProviderInstanceOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            TInstanceOptions instanceOptions = null;
            if (!string.IsNullOrEmpty(parsedQueue.ProviderName))
            {
                options.ParsedProviderInstances.TryGetValue(parsedQueue.ProviderName, out instanceOptions);
                if (instanceOptions == null)
                {
                    return;
                }

                parsedQueue.ProviderType = instanceOptions.Type;
            }

            options.BindQueueOptions(parsedQueue, instanceOptions);
        }

        private static TOptions BindOptions<TOptions>(KeyValuePair<string, IConfigurationSection> kvp)
            where TOptions : class, INamedElementOptions, new()
        {
            var options = new TOptions
            {
                Name = kvp.Key,
            };

            ConfigurationBinder.Bind(kvp.Value, options);
            return options;
        }

        public static TQueueOptions GetQueueConfiguration<TInstanceOptions, TQueueOptions>(this IParsedOptions<TInstanceOptions, TQueueOptions> parsedOptions, string queueName, bool throwIfNotFound = true)
            where TInstanceOptions : class, IProviderInstanceOptions
            where TQueueOptions : class, IQueueOptions
        {
            parsedOptions.ParsedQueueOptions.TryGetValue(queueName, out var queueOptions);
            if (queueOptions != null)
            {
                return queueOptions;
            }

            if (throwIfNotFound)
            {
                throw new Exceptions.QueueNotFound(queueName);
            }

            return null;
        }

        public static TQueueOptions ParseQueueOptions<TParsedOptions, TInstanceOptions, TQueueOptions>(this IQueueOptions queueOptions, TParsedOptions options)
            where TParsedOptions : class, IParsedOptions<TInstanceOptions, TQueueOptions>, new()
            where TInstanceOptions : class, IProviderInstanceOptions, new()
            where TQueueOptions : class, IQueueOptions, new()
        {
            if (!(queueOptions is TQueueOptions parsedQueueOptions))
            {
                parsedQueueOptions = new TQueueOptions
                {
                    Name = queueOptions.Name,
                    ProviderName = queueOptions.ProviderName,
                    ProviderType = queueOptions.ProviderType
                };
            }

            parsedQueueOptions.Compute<TParsedOptions, TInstanceOptions, TQueueOptions>(options);
            return parsedQueueOptions;
        }
    }
}
