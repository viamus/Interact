using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("cloud_instance")]
    public partial class CloudInstance
    {
        public CloudInstance()
        {
            CloudClientInstance = new HashSet<CloudClientInstance>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("threadgroup")]
        public string Threadgroup { get; set; }
        [Column("consumer_status_id")]
        public int ConsumerStatusId { get; set; }
        [Column("cloud_consumer_configuration_id")]
        public int CloudConsumerConfigurationId { get; set; }
        [Column("worker_configuration_id")]
        public int WorkerConfigurationId { get; set; }

        [ForeignKey("CloudConsumerConfigurationId")]
        [InverseProperty("CloudInstance")]
        public CloudConsumerConfiguration CloudConsumerConfiguration { get; set; }
        [ForeignKey("ConsumerStatusId")]
        [InverseProperty("CloudInstance")]
        public ConsumerStatus ConsumerStatus { get; set; }
        [ForeignKey("WorkerConfigurationId")]
        [InverseProperty("CloudInstance")]
        public WorkerConfiguration WorkerConfiguration { get; set; }
        [InverseProperty("CloudInstance")]
        public ICollection<CloudClientInstance> CloudClientInstance { get; set; }
    }
}
