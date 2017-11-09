using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeekLearning.Events.Model;
using GeekLearning.Events.SampleEventsModels.Models;

namespace GeekLearning.Events.SampleEventModels.Queuer
{
    public class TestQueuer : EventQueuerBase, IEventQueuer
    {
        private const string queueName = "queue1";

        public string Name => queueName;

        public string MessageTypeName => nameof(EventTest);

        public TestQueuer(IEventFactory eventFactory) : base(queueName, eventFactory)
        {

        }

        public Task CommitAsync()
        {
            return base.Queuer.CommitAsync();
        }

        public void Dispose()
        {
            this.CommitAsync();
        }

        public IEnumerable<EventBase> flush()
        {
            return base.Queuer.flush();
        }

        public void QueueEvent<TEvent>(TEvent Event) where TEvent : EventBase
        {
            base.Queuer.QueueEvent(Event);
        }
    }
}
