using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("worker_configuration")]
    public partial class WorkerConfiguration
    {
        public WorkerConfiguration()
        {
            CloudInstance = new HashSet<CloudInstance>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("worker_type_id")]
        public int WorkerTypeId { get; set; }
        [Required]
        [Column("name", TypeName = "character varying(200)")]
        public string Name { get; set; }
        [Required]
        [Column("json")]
        public string Json { get; set; }
        [Column("blueprint")]
        public bool Blueprint { get; set; }

        [ForeignKey("WorkerTypeId")]
        [InverseProperty("WorkerConfiguration")]
        public WorkerType WorkerType { get; set; }
        [InverseProperty("WorkerConfiguration")]
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
