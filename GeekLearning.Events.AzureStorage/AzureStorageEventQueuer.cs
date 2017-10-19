namespace GeekLearning.Events.AzureStorage
{
    using GeekLearning.Events;
    using GeekLearning.Events.AzureStorage.Configuration;
    using GeekLearning.Events.Model;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AzureStorageEventQueuer : IEventQueuer
    {
        private readonly AzureStorageQueueOptions queueOptions;
        private readonly CloudStorageAccount storageAccount;
        private readonly Queue<EventBase> queue = new Queue<EventBase>();

        public string Name => this.queueOptions.Name;

        public AzureStorageEventQueuer(AzureStorageQueueOptions queueOptions)
        {
            this.queueOptions = queueOptions;
            this.storageAccount = CloudStorageAccount.Parse(queueOptions.ConnectionString);
        }

        public void QueueEvent<TEvent>(TEvent Event) where TEvent : EventBase
        {
            if (!this.queue.Any(e => e == Event))
            {
                this.queue.Enqueue(Event);
            }
        }

        public async Task CommitAsync()
        {
            await this.storageAccount.EnsureAuthorizationsQueueIsCreatedAsync(queueOptions.QueueName);

            CloudQueueClient queueClient = this.storageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = queueClient.GetQueueReference(this.queueOptions.QueueName);

            List<Task> addMessagesTaskList = new List<Task>();
            foreach (EventBase Event in this.queue)
            {
                addMessagesTaskList.Add(
                    cloudQueue.AddMessageAsync(
                        new CloudQueueMessage(JsonConvert.SerializeObject(Event))));
            }

            this.queue.Clear();

            await Task.WhenAll(addMessagesTaskList);
        }

        public void Dispose()
        {
            this.CommitAsync();
        }

        public IEnumerable<EventBase> flush()
        {
            List<EventBase> messagesList = new List<EventBase>();
            foreach (EventBase Event in this.queue)
            {
                messagesList.Add(Event);
            }

            this.queue.Clear();

            return messagesList;
        }

    }
}
