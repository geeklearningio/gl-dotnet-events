namespace GeekLearning.Events.SampleWebJob
{
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class ServiceProviderJobActivator : IJobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderJobActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T CreateInstance<T>()
        {
            var serviceScope = this.serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var instance = ActivatorUtilities.CreateInstance<T>(serviceScope.ServiceProvider, serviceScope);
            if (instance == null)
            {
                throw new ArgumentException($"Unable to resolve type '{typeof(T)}'. Have you registered it?");
            }

            return instance;
        }
    }
}
