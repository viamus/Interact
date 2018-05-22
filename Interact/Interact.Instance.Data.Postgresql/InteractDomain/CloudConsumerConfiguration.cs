using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class CloudConsumerConfiguration
    {
        public CloudConsumerConfiguration()
        {
            CloudInstance = new HashSet<CloudInstance>();
        }

        public int Id { get; set; }
        public int ConsumerTypeId { get; set; }
        public int CloudQueueConfigurationId { get; set; }
        public int ConsumerServerStatusId { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }

        public CloudQueueConfiguration CloudQueueConfiguration { get; set; }
        public ConsumerServerStatus ConsumerServerStatus { get; set; }
        public ConsumerType ConsumerType { get; set; }
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
