namespace GeekLearning.Events.SampleEventsModels.Models
{
    using GeekLearning.Events.Model;

    public class EventBaseSample : EventBase
    {
        public EventBaseSample()
        {

        }

        public EventBaseSample (string name) : base($"{name}")
        {

        }
    }
}
