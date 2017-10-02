namespace GeekLearning.Events
{
    using GeekLearning.Events.Model;
    using System.Threading.Tasks;

    public interface IEventQueuer
    {
        void QueueEvent<TEvent>(TEvent Event) where TEvent : EventBase;

        Task CommitAsync();
    }
}
