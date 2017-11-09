using GeekLearning.Events.SampleEventsModels.Models;
using System;

namespace GeekLearning.Events.SampleEventsModels.Events
{
    public class EventAdvanceHandler : IEventHandler<EventAdvanced>
    {
        public void ExecuteAsync(EventAdvanced evenBase)
        {
            Console.WriteLine("ADVANCED BITCH");
        }
    }
}
