namespace GeekLearning.Events.Configuration.Provider
{
    public interface IProviderInstanceOptions : INamedElementOptions
    {
        string Type { get; }
    }
}
