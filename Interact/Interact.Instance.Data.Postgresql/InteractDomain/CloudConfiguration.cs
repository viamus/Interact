using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class CloudConfiguration
    {
        public CloudConfiguration()
        {
            CloudQueueConfiguration = new HashSet<CloudQueueConfiguration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }

        public ICollection<CloudQueueConfiguration> CloudQueueConfiguration { get; set; }
    }
}
