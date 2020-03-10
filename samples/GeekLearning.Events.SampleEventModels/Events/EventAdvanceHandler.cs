namespace GeekLearning.Events.SampleEventsModels.Events
{
    using GeekLearning.Events.SampleEventsModels.Models;
    using System;
    using System.Threading.Tasks;

    public class EventAdvanceHandler : IEventHandler<EventAdvanced>
    {
        public Task ExecuteAsync(EventAdvanced evenBase)
        {
            Console.WriteLine("ADVANCED Event");

            return Task.CompletedTask;
        }
    }
}
