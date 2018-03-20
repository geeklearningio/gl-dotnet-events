namespace GeekLearning.Events
{
    using GeekLearning.Events.Model;
    using System.Threading.Tasks;

    public interface IEventReceiver
    {
        Task ReceiveAsync<Tevent>(Tevent eventBase) where Tevent : EventBase;
    }
}
