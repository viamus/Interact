using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    [Table("cloud_configuration")]
    public partial class CloudConfiguration
    {
        public CloudConfiguration()
        {
            CloudQueueConfiguration = new HashSet<CloudQueueConfiguration>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("json")]
        public string Json { get; set; }

        [InverseProperty("CloudConfiguration")]
        public ICollection<CloudQueueConfiguration> CloudQueueConfiguration { get; set; }
    }
}
