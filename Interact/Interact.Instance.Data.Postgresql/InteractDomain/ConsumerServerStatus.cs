using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class ConsumerServerStatus
    {
        public ConsumerServerStatus()
        {
            CloudConsumerConfiguration = new HashSet<CloudConsumerConfiguration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
    }
}
