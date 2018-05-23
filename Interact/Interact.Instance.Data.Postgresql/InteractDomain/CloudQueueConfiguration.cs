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
            CloudConsumerConfiguration = new HashSet<CloudConsumerConfiguration>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("cloud_configuration_id")]
        public int CloudConfigurationId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("json")]
        public string Json { get; set; }

        [ForeignKey("CloudConfigurationId")]
        [InverseProperty("CloudQueueConfiguration")]
        public CloudConfiguration CloudConfiguration { get; set; }
        [InverseProperty("CloudQueueConfiguration")]
        public ICollection<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
    }
}
