namespace GeekLearning.Events.SampleWebJob
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;

    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static void Main()
        {
            var applicationEnvironment = PlatformServices.Default.Application;

            var builder = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.version.json")
                .AddJsonFile("appsettings.development.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var config = new JobHostConfiguration
            {
                StorageConnectionString = Configuration.GetConnectionString("AzureWebJobsStorage"),
                DashboardConnectionString = Configuration.GetConnectionString("AzureWebJobsDashboard"),
                JobActivator = new ServiceProviderJobActivator(serviceProvider)
            };

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddEventReceiver();
        }
    }
}
