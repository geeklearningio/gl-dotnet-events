namespace GeekLearning.Events.Exceptions
{
    using System;

    public class QueueNotFound : Exception
    {
        public QueueNotFound(string queueName)
            : base($"The queue '{queueName}' was not found. Did you configure it properly in appsettings.json ?")
        {
        }
    }
}
