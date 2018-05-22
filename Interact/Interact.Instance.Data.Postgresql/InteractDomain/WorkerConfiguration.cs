using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class WorkerConfiguration
    {
        public WorkerConfiguration()
        {
            CloudInstance = new HashSet<CloudInstance>();
        }

        public int Id { get; set; }
        public int WorkerTypeId { get; set; }
        public string Threadgroup { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }

        public WorkerType WorkerType { get; set; }
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
