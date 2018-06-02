using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("cloud_client_instance")]
    public partial class CloudClientInstance
    {
        [Key]
        [Column("identifier", TypeName = "character varying(50)")]
        public string Identifier { get; set; }
        [Column("cloud_instance_id")]
        public int CloudInstanceId { get; set; }
        [Column("consumer_status_id")]
        public int ConsumerStatusId { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }

        [ForeignKey("CloudInstanceId")]
        [InverseProperty("CloudClientInstance")]
        public CloudInstance CloudInstance { get; set; }
        [ForeignKey("ConsumerStatusId")]
        [InverseProperty("CloudClientInstance")]
        public ConsumerStatus ConsumerStatus { get; set; }
    }
}
