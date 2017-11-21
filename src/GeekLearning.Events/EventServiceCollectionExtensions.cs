namespace GeekLearning.Events
{
    using GeekLearning.Events.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Collections.Generic;

    public static class EventServiceCollectionExtensions
    {
        public static IServiceCollection AddEvent(this IServiceCollection services)
        {
            services.TryAddTransient<IEventFactory, Internal.EventFactory>();
            return services;
        }

        public static IServiceCollection AddEvent(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            return services
                .Configure<EventOptions>(configurationSection)
                .AddEvent();
        }

        public static IServiceCollection AddEvent(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            return services
                .Configure<EventOptions>(configurationRoot.GetSection(EventOptions.DefaultConfigurationSectionName))
                .Configure<EventOptions>(eventOptions =>
                {
                    var connectionStrings = new Dictionary<string, string>();
                    ConfigurationBinder.Bind(configurationRoot.GetSection("ConnectionStrings"), connectionStrings);

                    if (eventOptions.ConnectionStrings != null)
                    {
                        foreach (var existingConnectionString in eventOptions.ConnectionStrings)
                        {
                            connectionStrings[existingConnectionString.Key] = existingConnectionString.Value;
                        }
                    }

                    eventOptions.ConnectionStrings = connectionStrings;
                })
                .AddEvent();
        }

        public static void AddEventReceiver(this IServiceCollection services)
        {
            services.AddScoped<IEventReceiver, EventReceiver>();
        }

    }
}
