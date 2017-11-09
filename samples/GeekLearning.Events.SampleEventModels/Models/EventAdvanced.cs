namespace GeekLearning.Events.SampleEventsModels.Models
{

    public class EventAdvanced : EventBaseSample
    {
        public EventAdvanced()
        {

        }

        public EventAdvanced(string name)
            : base($"{name}")
        {

        }
    }
}
