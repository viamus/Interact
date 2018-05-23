using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("cloud_consumer_configuration")]
    public partial class CloudConsumerConfiguration
    {
        public CloudConsumerConfiguration()
        {
            CloudInstance = new HashSet<CloudInstance>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("consumer_type_id")]
        public int ConsumerTypeId { get; set; }
        [Column("cloud_queue_configuration_id")]
        public int CloudQueueConfigurationId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("json")]
        public string Json { get; set; }

        [ForeignKey("CloudQueueConfigurationId")]
        [InverseProperty("CloudConsumerConfiguration")]
        public CloudQueueConfiguration CloudQueueConfiguration { get; set; }
        [ForeignKey("ConsumerTypeId")]
        [InverseProperty("CloudConsumerConfiguration")]
        public ConsumerType ConsumerType { get; set; }
        [InverseProperty("CloudConsumerConfiguration")]
        public ICollection<CloudInstance> CloudInstance { get; set; }
    }
}
