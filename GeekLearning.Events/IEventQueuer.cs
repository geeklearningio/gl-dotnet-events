namespace GeekLearning.Events
{
    using GeekLearning.Events.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventQueuer : IDisposable
    {
        string Name { get; }

        void QueueEvent<TEvent>(TEvent Event) where TEvent : EventBase;

        Task CommitAsync();

        IEnumerable<EventBase> flush();
    }
}
