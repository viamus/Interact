using Interact.Library.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Metadata
{
    public class CloudInstance
    {
        public int Id { get; set; }

        public string Threadgroup { get; set; }

        public CloudConsumerConfiguration CloudConsumerConfiguration { get; set; }

        public ConsumerStatus ConsumerStatus { get; set; }

        public WorkerConfiguration WorkerConfiguration { get; set; }

        public ICollection<CloudClientInstance> CloudClientInstance { get; set; }
    }
}
