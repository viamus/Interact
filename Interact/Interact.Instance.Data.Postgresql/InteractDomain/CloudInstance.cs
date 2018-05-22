using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class CloudInstance
    {
        public int Id { get; set; }
        public string Threadgroup { get; set; }
        public int CloudConsumerConfigurationId { get; set; }
        public int WorkerConfigurationId { get; set; }

        public CloudConsumerConfiguration CloudConsumerConfiguration { get; set; }
        public WorkerConfiguration WorkerConfiguration { get; set; }
    }
}
