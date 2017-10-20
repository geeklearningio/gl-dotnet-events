using System;
using System.Collections.Generic;
using System.Text;
using GeekLearning.Events.Model;

namespace GeekLearning.Events.InMemory.Internal
{
    public class QueueStorageInMemory : IQueueStorageInMemory
    {
        private Queue<EventBase> queue = new Queue<EventBase>();

        public void Add(EventBase eventBase)
        {
            this.queue.Enqueue(eventBase);
        }

        public EventBase GetEvent()
        {
            return this.queue.Dequeue();
        }
    }
}
