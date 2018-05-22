using System;
using System.Collections.Generic;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class WorkerType
    {
        public WorkerType()
        {
            WorkerConfiguration = new HashSet<WorkerConfiguration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string Version { get; set; }

        public ICollection<WorkerConfiguration> WorkerConfiguration { get; set; }
    }
}
