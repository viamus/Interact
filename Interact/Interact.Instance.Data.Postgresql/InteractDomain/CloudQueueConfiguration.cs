using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("cloud_queue_configuration")]
    public partial class CloudQueueConfiguration
    {
        public CloudQueueConfiguration()
        {
            CloudInstance = new HashSet<CloudInstance>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("cloud_configuration_id")]
        public int CloudConfigurationId { get; set; }
        [Required]
        [Column("name", TypeName = "character varying(200)")]
        public string Name { get; set; }
        [Required]
        [Column("json")]
        public string Json { get; set; }
        [Column("blueprint")]
        public bool Blueprint { get; set; }

        [ForeignKey("CloudConfigurationId")]
        [InverseProperty("CloudQueueConfiguration")]
        public CloudConfiguration CloudConfiguration { get; set; }
        [InverseProperty("CloudQueueConfiguration")]
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
