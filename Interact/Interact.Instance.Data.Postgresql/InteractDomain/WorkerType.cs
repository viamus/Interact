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
        [Column("name", TypeName = "character varying(100)")]
        public string Name { get; set; }
        [Required]
        [Column("assembly", TypeName = "character varying(500)")]
        public string Assembly { get; set; }

        [InverseProperty("WorkerType")]
        public ICollection<WorkerConfiguration> WorkerConfiguration { get; set; }
    }
}
