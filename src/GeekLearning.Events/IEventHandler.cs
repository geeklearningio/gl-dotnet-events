namespace GeekLearning.Events
{
    using GeekLearning.Events.Model;
    using System.Threading.Tasks;

    public interface IEventHandler<TEvent> where TEvent : EventBase
    {
        Task<EventBase> ExecuteAsync(TEvent evenBase);
    }
}
