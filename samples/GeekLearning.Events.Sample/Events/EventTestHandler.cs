using System.Threading.Tasks;
using GeekLearning.Events.Model;
using GeekLearning.Events.Sample.Models;
using System;

namespace GeekLearning.Events.Sample.Events
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
