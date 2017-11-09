using GeekLearning.Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
