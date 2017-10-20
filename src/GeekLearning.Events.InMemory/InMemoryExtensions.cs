using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using GeekLearning.Events.InMemory.Internal;
using GeekLearning.Events.InMemory.Configuration;
using GeekLearning.Events.Internal;

namespace GeekLearning.Events.InMemory
{
    public static class InMemoryExtensions
    {
        public static IServiceCollection AddInMemoryQueue(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConfigureOptions<InMemoryParsedOptions>, ConfigureProviderOptions<InMemoryParsedOptions, InMemoryProviderInstanceOptions, InMemoryQueueOptions>>()
                .AddInMemoryQueueServices()
                .AddInMemoryQueueStorage();
        }

        internal static IServiceCollection AddInMemoryQueueServices(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Scoped<IEventProvider, InMemoryProvider>());
            return services;
        }

        internal static IServiceCollection AddInMemoryQueueStorage(this IServiceCollection services)
        {
            return services
                .AddSingleton<IQueueStorageInMemory, QueueStorageInMemory>();
        }

    }
}
