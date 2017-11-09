using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLearning.Events.WebJobSample
{
    public class ServiceProviderJobActivator : IJobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderJobActivator (IServiceProvider serviceProvider)
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
