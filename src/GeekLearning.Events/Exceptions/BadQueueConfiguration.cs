namespace GeekLearning.Events.Exceptions
{
    using System;

    public class BadQueueConfiguration : Exception
    {
        public BadQueueConfiguration(string queueName)
            : base($"The Queue '{queueName}' was not properly configured.")
        {
        }

        public BadQueueConfiguration(string queueName, string details)
            : base($"The Queue '{queueName}' was not properly configured. {details}")
        {
        }
    }
}
