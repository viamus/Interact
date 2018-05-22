using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class InteractContext : DbContext
    {
        public virtual DbSet<CloudConfiguration> CloudConfiguration { get; set; }
        public virtual DbSet<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
        public virtual DbSet<CloudInstance> CloudInstance { get; set; }
        public virtual DbSet<CloudQueueConfiguration> CloudQueueConfiguration { get; set; }
        public virtual DbSet<ConsumerServerStatus> ConsumerServerStatus { get; set; }
        public virtual DbSet<ConsumerType> ConsumerType { get; set; }
        public virtual DbSet<WorkerConfiguration> WorkerConfiguration { get; set; }
        public virtual DbSet<WorkerType> WorkerType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               //optionsBuilder.UseNpgsql(@"Host=localhost;Database=interact;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CloudConfiguration>(entity =>
            {
                entity.ToTable("cloud_configuration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("json");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CloudConsumerConfiguration>(entity =>
            {
                entity.ToTable("cloud_consumer_configuration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CloudQueueConfigurationId).HasColumnName("cloud_queue_configuration_id");

                entity.Property(e => e.ConsumerServerStatusId).HasColumnName("consumer_server_status_id");

                entity.Property(e => e.ConsumerTypeId).HasColumnName("consumer_type_id");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("json");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.CloudQueueConfiguration)
                    .WithMany(p => p.CloudConsumerConfiguration)
                    .HasForeignKey(d => d.CloudQueueConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_consumer_configuration_cloud_queue_configuration_id");

                entity.HasOne(d => d.ConsumerServerStatus)
                    .WithMany(p => p.CloudConsumerConfiguration)
                    .HasForeignKey(d => d.ConsumerServerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_consumer_configuration_consumer_server_status_id");

                entity.HasOne(d => d.ConsumerType)
                    .WithMany(p => p.CloudConsumerConfiguration)
                    .HasForeignKey(d => d.ConsumerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_consumer_configuration_consumer_type_id");
            });

            modelBuilder.Entity<CloudInstance>(entity =>
            {
                entity.ToTable("cloud_instance");

                entity.HasIndex(e => e.Threadgroup)
                    .HasName("cloud_instance_threadgroup_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CloudConsumerConfigurationId).HasColumnName("cloud_consumer_configuration_id");

                entity.Property(e => e.Threadgroup).HasColumnName("threadgroup");

                entity.Property(e => e.WorkerConfigurationId).HasColumnName("worker_configuration_id");

                entity.HasOne(d => d.CloudConsumerConfiguration)
                    .WithMany(p => p.CloudInstance)
                    .HasForeignKey(d => d.CloudConsumerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_instance_cloud_consumer_configuration_id");

                entity.HasOne(d => d.WorkerConfiguration)
                    .WithMany(p => p.CloudInstance)
                    .HasForeignKey(d => d.WorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_instance_worker_configuration_id");
            });

            modelBuilder.Entity<CloudQueueConfiguration>(entity =>
            {
                entity.ToTable("cloud_queue_configuration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CloudConfigurationId).HasColumnName("cloud_configuration_id");

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("json");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.CloudConfiguration)
                    .WithMany(p => p.CloudQueueConfiguration)
                    .HasForeignKey(d => d.CloudConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_queue_configuration_cloud_configuration_id");
            });

            modelBuilder.Entity<ConsumerServerStatus>(entity =>
            {
                entity.ToTable("consumer_server_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ConsumerType>(entity =>
            {
                entity.ToTable("consumer_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Assembly)
                    .IsRequired()
                    .HasColumnName("assembly");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnName("version");
            });

            modelBuilder.Entity<WorkerConfiguration>(entity =>
            {
                entity.ToTable("worker_configuration");

                entity.HasIndex(e => e.Threadgroup)
                    .HasName("worker_configuration_threadgroup_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Json)
                    .IsRequired()
                    .HasColumnName("json");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Threadgroup).HasColumnName("threadgroup");

                entity.Property(e => e.WorkerTypeId).HasColumnName("worker_type_id");

                entity.HasOne(d => d.WorkerType)
                    .WithMany(p => p.WorkerConfiguration)
                    .HasForeignKey(d => d.WorkerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_worker_configuration_worker_type_id");
            });

            modelBuilder.Entity<WorkerType>(entity =>
            {
                entity.ToTable("worker_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Assembly)
                    .IsRequired()
                    .HasColumnName("assembly");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnName("version");
            });
        }
    }
}
