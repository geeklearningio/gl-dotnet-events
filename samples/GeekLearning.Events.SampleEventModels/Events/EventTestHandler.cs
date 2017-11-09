namespace GeekLearning.Events.SampleEventsModels.Events
{
    using GeekLearning.Events.SampleEventsModels.Models;
    using System;

    public class EventTestHandler : IEventHandler<EventTest>
    {
        public void ExecuteAsync(EventTest evenBase)
        {
            Console.WriteLine("Triggered");
            Console.WriteLine("TEST EVENT");
        }
    }
}
