using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Interact.Instance.Data.Postgresql.InteractDomain
{
    public partial class InteractContext : DbContext
    {
        public virtual DbSet<CloudClientInstance> CloudClientInstance { get; set; }
        public virtual DbSet<CloudConfiguration> CloudConfiguration { get; set; }
        public virtual DbSet<CloudConsumerConfiguration> CloudConsumerConfiguration { get; set; }
        public virtual DbSet<CloudInstance> CloudInstance { get; set; }
        public virtual DbSet<CloudQueueConfiguration> CloudQueueConfiguration { get; set; }
        public virtual DbSet<ConsumerStatus> ConsumerStatus { get; set; }
        public virtual DbSet<ConsumerType> ConsumerType { get; set; }
        public virtual DbSet<WorkerConfiguration> WorkerConfiguration { get; set; }
        public virtual DbSet<WorkerType> WorkerType { get; set; }

        public InteractContext(DbContextOptions<InteractContext> options) :
            base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=interact;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CloudClientInstance>(entity =>
            {
                entity.Property(e => e.Identifier).ValueGeneratedNever();

                entity.HasOne(d => d.CloudInstance)
                    .WithMany(p => p.CloudClientInstance)
                    .HasForeignKey(d => d.CloudInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_client_instance_cloud_instance_id");

                entity.HasOne(d => d.ConsumerStatus)
                    .WithMany(p => p.CloudClientInstance)
                    .HasForeignKey(d => d.ConsumerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_client_instance_consumer_status_id");
            });

            modelBuilder.Entity<CloudConfiguration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<CloudConsumerConfiguration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.CloudQueueConfiguration)
                    .WithMany(p => p.CloudConsumerConfiguration)
                    .HasForeignKey(d => d.CloudQueueConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_consumer_configuration_cloud_queue_configuration_id");

                entity.HasOne(d => d.ConsumerType)
                    .WithMany(p => p.CloudConsumerConfiguration)
                    .HasForeignKey(d => d.ConsumerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_consumer_configuration_consumer_type_id");
            });

            modelBuilder.Entity<CloudInstance>(entity =>
            {
                entity.HasIndex(e => e.Threadgroup)
                    .HasName("cloud_instance_threadgroup_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.CloudConsumerConfiguration)
                    .WithMany(p => p.CloudInstance)
                    .HasForeignKey(d => d.CloudConsumerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_instance_cloud_consumer_configuration_id");

                entity.HasOne(d => d.ConsumerStatus)
                    .WithMany(p => p.CloudInstance)
                    .HasForeignKey(d => d.ConsumerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_instance_consumer_status_id");

                entity.HasOne(d => d.WorkerConfiguration)
                    .WithMany(p => p.CloudInstance)
                    .HasForeignKey(d => d.WorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_instance_worker_configuration_id");
            });

            modelBuilder.Entity<CloudQueueConfiguration>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.CloudConfiguration)
                    .WithMany(p => p.CloudQueueConfiguration)
                    .HasForeignKey(d => d.CloudConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cloud_queue_configuration_cloud_configuration_id");
            });

            modelBuilder.Entity<ConsumerStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ConsumerType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<WorkerConfiguration>(entity =>
            {
                entity.HasIndex(e => e.Threadgroup)
                    .HasName("worker_configuration_threadgroup_key")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.WorkerType)
                    .WithMany(p => p.WorkerConfiguration)
                    .HasForeignKey(d => d.WorkerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_worker_configuration_worker_type_id");
            });

            modelBuilder.Entity<WorkerType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
