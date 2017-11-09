namespace GeekLearning.Events.Sample
{
    using GeekLearning.Events.Sample.Events;
    using GeekLearning.Events.Sample.Models;
    using GeekLearning.Events.SampleEventModels.Queuer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventQueuers (this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IEventQueuer, TestQueuer>());
            return services;
        }

        public static IServiceCollection AddEventsHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandler<EventTest>, EventTestHandler>();
            services.AddSingleton<IEventHandler<EventAdvanced>, EventAdvanceHandler>();
            return services;
        }
    }
}
