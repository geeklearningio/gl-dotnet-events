using GeekLearning.Events.SampleEventsModels.Models;
using System;

namespace GeekLearning.Events.SampleEventsModels.Events
{
    public class EventTestHandler : IEventHandler<EventTest>
    {
        public void ExecuteAsync(EventTest evenBase)
        {
            Console.WriteLine("Triggered");
            Console.WriteLine("TEST EVENT");
        }
    }
}
