namespace GeekLearning.Events.Configuration
{
    public interface IOptionError
    {
        string PropertyName { get; }

        string ErrorMessage { get; }
    }
}