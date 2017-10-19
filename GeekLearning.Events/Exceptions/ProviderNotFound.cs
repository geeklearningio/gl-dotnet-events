namespace GeekLearning.Events.Exceptions
{
    using System;

    class ProviderNotFound : Exception
    {
        public ProviderNotFound(string providerName)
            : base($"The provider '{providerName}' was not found.")
        {
        }
    }
}
