namespace GeekLearning.Events.SampleEventsModels.Models
{
    public class EventTest : EventBaseSample
    {
        public EventTest()
        {

        }

        public EventTest(string name)
            : base($"{name}")
        {

        }
    }
}
