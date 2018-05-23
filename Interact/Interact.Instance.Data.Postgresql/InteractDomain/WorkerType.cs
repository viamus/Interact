using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("worker_type")]
    public partial class WorkerType
    {
        public WorkerType()
        {
            WorkerConfiguration = new HashSet<WorkerConfiguration>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("assembly")]
        public string Assembly { get; set; }
        [Required]
        [Column("version")]
        public string Version { get; set; }

        [InverseProperty("WorkerType")]
        public ICollection<WorkerConfiguration> WorkerConfiguration { get; set; }
    }
}
