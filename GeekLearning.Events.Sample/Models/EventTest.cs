using GeekLearning.Events.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.Events.Sample.Models
{
    public class EventTest : EventBase
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
