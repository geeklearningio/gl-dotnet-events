using GeekLearning.Events.Model;

namespace GeekLearning.Events.Sample.Models
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
