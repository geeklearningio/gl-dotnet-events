namespace GeekLearning.Events.SampleEventsModels.Events
{
    using GeekLearning.Events.SampleEventsModels.Models;
    using System;

    public class EventAdvanceHandler : IEventHandler<EventAdvanced>
    {
        public void ExecuteAsync(EventAdvanced evenBase)
        {
            Console.WriteLine("ADVANCED Event");
        }
    }
}
