using GeekLearning.Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Events.SampleEventsModels.Models
{
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
