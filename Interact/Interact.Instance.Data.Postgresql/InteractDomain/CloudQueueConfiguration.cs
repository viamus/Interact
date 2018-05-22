using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class CloudQueueConfiguration
    {
        public CloudQueueConfiguration()
        {
            CloudConsumerConfiguration = new HashSet<CloudConsumerConfiguration>();
        }

        public int Id { get; set; }
        public int CloudConfigurationId { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }

        public CloudConfiguration CloudConfiguration { get; set; }
        public ICollection<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
    }
}
