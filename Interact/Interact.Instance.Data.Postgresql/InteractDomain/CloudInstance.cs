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
        [Column("threadgroup", TypeName = "character varying(200)")]
        public string Threadgroup { get; set; }
        [Column("consumer_status_id")]
        public int ConsumerStatusId { get; set; }
        [Column("cloud_queue_configuration_id")]
        public int CloudQueueConfigurationId { get; set; }
        [Column("worker_configuration_id")]
        public int WorkerConfigurationId { get; set; }
        [Column("blueprint")]
        public bool Blueprint { get; set; }

        [ForeignKey("CloudQueueConfigurationId")]
        [InverseProperty("CloudInstance")]
        public CloudQueueConfiguration CloudQueueConfiguration { get; set; }
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
