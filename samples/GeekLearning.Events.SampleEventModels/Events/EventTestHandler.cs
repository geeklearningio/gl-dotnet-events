namespace GeekLearning.Events.SampleEventsModels.Events
{
    using GeekLearning.Events.SampleEventsModels.Models;
    using System;
    using System.Threading.Tasks;

    public class EventTestHandler : IEventHandler<EventTest>
    {
        public Task ExecuteAsync(EventTest evenBase)
        {
            Console.WriteLine("Triggered");
            Console.WriteLine("TEST EVENT");

            return Task.CompletedTask;
        }
    }
}
