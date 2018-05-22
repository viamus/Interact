using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class ConsumerType
    {
        public ConsumerType()
        {
            CloudConsumerConfiguration = new HashSet<CloudConsumerConfiguration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Version { get; set; }

        public ICollection<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
    }
}
