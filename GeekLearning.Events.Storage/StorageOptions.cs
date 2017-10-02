using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLearning.Events.Storage
{
    public class StorageOptions
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
