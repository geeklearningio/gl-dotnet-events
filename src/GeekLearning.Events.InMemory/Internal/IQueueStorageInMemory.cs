namespace GeekLearning.Events.InMemory.Internal
{
    using GeekLearning.Events.Model;
    using System.Collections.Generic;

    public interface IQueueStorageInMemory
    {
        void Add(EventBase eventBase);

        EventBase GetEvent();
    }
}
