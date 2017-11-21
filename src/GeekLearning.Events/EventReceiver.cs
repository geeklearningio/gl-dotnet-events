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

            var getEventHandler = this.serviceProvider.GetService(queryType);

            await (Task)queryType.GetMethod("ExecuteAsync")
                .Invoke(getEventHandler, new object[] { eventBase });
        }
    }
}
