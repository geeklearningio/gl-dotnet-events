namespace GeekLearning.Events.SampleEventsModels.Models
{
    using GeekLearning.Events.Model;

    public class EventBaseSample : EventBase
    {
        public EventBaseSample()
        {

        }

        public EventBaseSample(string name)
        {
            this.Key = name;
        }

        public override string Key { get; }
    }
}
