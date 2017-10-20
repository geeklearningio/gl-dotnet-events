namespace GeekLearning.Events.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GeekLearning.Events.Model;
    using GeekLearning.Events.InMemory.Configuration;
    using System.Linq;
    using GeekLearning.Events.InMemory.Internal;

    public class InMemoryEventQueuer : IEventQueuer
    {
        private readonly InMemoryQueueOptions queueOptions;
        private readonly Queue<EventBase> queue = new Queue<EventBase>();
        private IQueueStorageInMemory storedQueue;

        public string Name => this.queueOptions.Name;

        public InMemoryEventQueuer(IQueueStorageInMemory storedQueue, InMemoryQueueOptions queueOptions)
        {
            this.storedQueue = storedQueue;
            this.queueOptions = queueOptions;
        }

        public void QueueEvent<TEvent>(TEvent Event) where TEvent : EventBase
        {
            if (!this.queue.Any(e => e == Event))
            {
                this.queue.Enqueue(Event);
            }
        }

        public Task CommitAsync()
        {
            foreach (EventBase eventBase in this.queue)
            {
                this.storedQueue.Add(eventBase);
            }

            this.queue.Clear();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.CommitAsync();
        }

        public IEnumerable<EventBase> flush()
        {
            List<EventBase> messagesList = new List<EventBase>();
            foreach (EventBase Event in this.queue)
            {
                messagesList.Add(Event);
            }

            this.queue.Clear();

            return messagesList;
        }
    }
}
