namespace GeekLearning.Events
{
    using System.Threading.Tasks;
    using GeekLearning.Events.Model;
    using System;

    public class EventReceiver : IEventReceiver
    {
        private readonly IServiceProvider serviceProvider;

        public EventReceiver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task ReceiveAsync<Tevent>(Tevent eventBase) where Tevent : EventBase
        {
            var test = eventBase.GetType();

            var queryType = typeof(IEventHandler<>)
                .MakeGenericType(new Type[] { eventBase.GetType() });

            var getEventHadler = this.serviceProvider.GetService(queryType);

            await (Task<EventBase>)queryType.GetMethod("ExecuteAsync")
                .Invoke(getEventHadler, new object[] { eventBase });
        }
    }
}
