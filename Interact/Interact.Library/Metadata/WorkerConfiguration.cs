using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Library.Metadata
{
    public class WorkerConfiguration
    {
        public int Id { get; set; }
        public int WorkerTypeId { get; set; }
        public string Threadgroup { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }

        public WorkerType WorkerType { get; set; }
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
