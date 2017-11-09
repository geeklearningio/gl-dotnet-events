using GeekLearning.Events.Sample.Models;
using System;

namespace GeekLearning.Events.Sample.Events
{
    public class EventAdvanceHandler : IEventHandler<EventAdvanced>
    {
        public void ExecuteAsync(EventAdvanced evenBase)
        {
            Console.WriteLine("ADVANCED BITCH");
        }
    }
}
