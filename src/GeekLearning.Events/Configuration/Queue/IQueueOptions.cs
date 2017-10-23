namespace GeekLearning.Events.Configuration.Queue
{
    using GeekLearning.Events.Configuration.Provider;
    using System.Collections.Generic;

    public interface IQueueOptions : INamedElementOptions
    {
        string ProviderName { get; set; }

        string ProviderType { get; set; }

        IEnumerable<IOptionError> Validate(bool throwOnError = true);


    }
}
