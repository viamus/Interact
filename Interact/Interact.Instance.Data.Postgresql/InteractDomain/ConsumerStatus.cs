using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("consumer_status")]
    public partial class ConsumerStatus
    {
        public ConsumerStatus()
        {
            CloudClientInstance = new HashSet<CloudClientInstance>();
            CloudInstance = new HashSet<CloudInstance>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "character varying(100)")]
        public string Name { get; set; }

        [InverseProperty("ConsumerStatus")]
        public ICollection<CloudClientInstance> CloudClientInstance { get; set; }
        [InverseProperty("ConsumerStatus")]
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
