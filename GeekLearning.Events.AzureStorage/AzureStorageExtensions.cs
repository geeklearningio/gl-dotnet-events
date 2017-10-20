namespace GeekLearning.Events.AzureStorage
{
    using GeekLearning.Events.AzureStorage.Configuration;
    using GeekLearning.Events.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;

    public static class AzureStorageExtensions
    {
        public static IServiceCollection AddAzureStorageQueue(this IServiceCollection services)
        {
            return services
                .AddScoped<IConfigureOptions<AzureStorageParsedOptions>, ConfigureProviderOptions<AzureStorageParsedOptions, AzureStorageProviderInstanceOptions, AzureStorageQueueOptions>>()
                .AddAzureStorageServices();
        }

        public static IServiceCollection AddAzureStorageServices(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IEventProvider, AzureStorageProvider>());
            return services;
        }


    }
}
