using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("consumer_type")]
    public partial class ConsumerType
    {
        public ConsumerType()
        {
            CloudConsumerConfiguration = new HashSet<CloudConsumerConfiguration>();
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

        [InverseProperty("ConsumerType")]
        public ICollection<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
    }
}
