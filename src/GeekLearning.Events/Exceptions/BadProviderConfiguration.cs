namespace GeekLearning.Events.Exceptions
{
    using System;

    public class BadProviderConfiguration : Exception
    {
        public BadProviderConfiguration(string providerName)
            : base($"The Provider \"{providerName}\" was not properly configured.")
        {
        }

        public BadProviderConfiguration(string providerName, string details)
            : base($"The Provider \"{providerName}\" was not properly configured. {details}")
        {
        }

    }
}
