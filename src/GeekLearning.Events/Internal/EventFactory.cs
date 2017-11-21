namespace GeekLearning.Events.Internal
{
    using System.Collections.Generic;
    using GeekLearning.Events.Configuration.Queue;
    using Microsoft.Extensions.Options;
    using System.Linq;
    using GeekLearning.Events.Configuration;

    class EventFactory : IEventFactory
    {
        private EventOptions options;
        private IReadOnlyDictionary<string, IEventProvider> queueProviders;

        public EventFactory(IEnumerable<IEventProvider> queueProviders, IOptions<EventOptions> options)
        {
            this.queueProviders = queueProviders.ToDictionary(qp => qp.Name, qp => qp);
            this.options = options.Value;
        }

        public IEventQueuer GetQueuer(string queueName, IQueueOptions configuration)
        {
            return this.GetProvider(configuration).BuildQueueProvider(queueName, configuration);
        }

        public IEventQueuer GetQueuer(string queueName)
        {
            return this.GetProvider(this.options.GetQueueConfiguration(queueName)).BuildQueueProvider(queueName);
        }

        public bool TryGetQueuer(string queueName, out IEventQueuer queuer)
        {
            var configuration = this.options.GetQueueConfiguration(queueName, throwIfNotFound: false);
            if (configuration != null)
            {
                var provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null)
                {
                    queuer = provider.BuildQueueProvider(queueName);
                    return true;
                }
            }

            queuer = null;
            return false;
        }

        public bool TryGetQueuer(string queueName, out IEventQueuer queuer, string providerName)
        {
            var configuration = this.options.GetQueueConfiguration(queueName, throwIfNotFound: false);
            if (configuration != null)
            {
                var provider = this.GetProvider(configuration, throwIfNotFound: false);
                if (provider != null && provider.Name == providerName)
                {
                    queuer = provider.BuildQueueProvider(queueName);
                    return true;
                }
            }

            queuer = null;
            return false;
        }

        private IEventProvider GetProvider(IQueueOptions configuration, bool throwIfNotFound = true)
        {
            string providerTypeName = null;
            if (!string.IsNullOrEmpty(configuration.ProviderType))
            {
                providerTypeName = configuration.ProviderType;
            }
            else if (!string.IsNullOrEmpty(configuration.ProviderName))
            {
                this.options.ParsedProviderInstances.TryGetValue(configuration.ProviderName, out var providersInstanceOptions);
                if (providersInstanceOptions != null)
                {
                    providerTypeName = providersInstanceOptions.Type;
                }
                else if (throwIfNotFound)
                {
                    throw new Exceptions.BadProviderConfiguration(configuration.ProviderName, "Unable to find it in the configuration ");
                }
            }
            else if (throwIfNotFound)
            {
                throw new Exceptions.BadQueueConfiguration(configuration.Name, "You must set either 'ProviderType or 'ProviderName' in event configuration.");
            }

            if (string.IsNullOrEmpty(providerTypeName))
            {
                return null;
            }

            this.queueProviders.TryGetValue(providerTypeName, out var provider);
            if (provider == null && throwIfNotFound)
            {
                throw new Exceptions.ProviderNotFound(providerTypeName);
            }

            return provider;
        }
    }
}
