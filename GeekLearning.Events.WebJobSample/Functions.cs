using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using GeekLearning.Events.Model;
using GeekLearning.Events.SampleEventsModels.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GeekLearning.Events.WebJobSample
{
    public class Functions
    {
        private readonly IEventReceiver eventReceiver;
        private readonly IServiceScope serviceScope;

        public Functions (IServiceScope serviceScope, IEventReceiver eventReceiver)
        {
            this.serviceScope = serviceScope;
            this.eventReceiver = eventReceiver;
        }
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public void ProcessQueueMessage([QueueTrigger("queue1")] string message, TextWriter log)
        {
            var test = JsonSerializer.CreateDefault();
            var jsonreader = new JsonTextReader(new StringReader(message));
            var deserialized = test.Deserialize<EventBaseSample>(jsonreader);

            Console.WriteLine(message);
            Console.WriteLine("ICI");
            //this.eventReceiver.ReceiveAsync(message);
        }
    }
}
