namespace GeekLearning.Events.SampleWebJob
{
    using System;
    using System.IO;
    using Microsoft.Azure.WebJobs;
    using GeekLearning.Events.SampleEventsModels.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class Functions
    {
        private readonly IEventReceiver eventReceiver;
        private readonly IServiceScope serviceScope;

        public Functions(IServiceScope serviceScope, IEventReceiver eventReceiver)
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
            //this.eventReceiver.ReceiveAsync(message);
        }
    }
}
