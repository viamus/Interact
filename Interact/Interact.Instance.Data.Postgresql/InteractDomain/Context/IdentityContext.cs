using Interact.Instance.Data.Postgresql.InteractDomain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "aspnet_users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp")
                    .IsConcurrencyToken();

                entity.Property<string>(e => e.Email)
                    .HasAnnotation("MaxLength", 256);

                entity.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");

                entity.Property(e => e.LockoutEnabled).HasColumnName("lockout_enabled");

                entity.Property(e => e.LockoutEnd).HasColumnName("lockout_end");

                entity.Property(e => e.NormalizedEmail).HasColumnName("normalized_email")
                    .HasAnnotation("MaxLength", 256);

                entity.Property(e => e.NormalizedUserName).HasColumnName("normalized_user_name")
                    .HasAnnotation("MaxLength", 256);

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");

                entity.Property(e => e.SecurityStamp).HasColumnName("security_stamp");

                entity.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");

                entity.Property(e => e.UserName).HasColumnName("user_name")
                    .HasAnnotation("MaxLength", 256);

                entity.HasKey(e => e.Id);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "aspnet_roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp")
                    .IsConcurrencyToken();

                entity.Property(e => e.Name).HasColumnName("name")
                    .HasAnnotation("MaxLength", 256);

                entity.Property(e => e.NormalizedName).HasColumnName("normalized_name")
                    .HasAnnotation("MaxLength", 256);

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.NormalizedName).HasName("role_name_index");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("aspnet_user_roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("aspnet_user_claims");

                entity.Property(e => e.Id).HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClaimType).HasColumnName("claim_type");

                entity.Property(e => e.ClaimValue).HasColumnName("claim_value");

                entity.Property(e => e.UserId).HasColumnName("user_id")
                    .IsRequired();

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id).HasName("user_index");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("aspnet_user_logins");

                entity.Property(e => e.LoginProvider).HasColumnName("login_provider");

                entity.Property(e => e.ProviderKey).HasColumnName("provider_key");

                entity.Property(e => e.ProviderDisplayName).HasColumnName("provider_display_name");

                entity.Property(e => e.UserId).HasColumnName("user_id")
                    .IsRequired();

                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("aspnet_role_claims");

                entity.Property(e => e.Id).HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClaimType).HasColumnName("claim_type");

                entity.Property(e => e.ClaimValue).HasColumnName("claim_value");

                entity.Property(e => e.RoleId).HasColumnName("role_id")
                    .IsRequired();

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("aspnet_user_tokens");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.LoginProvider).HasColumnName("login_provider");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });
        }
    }
}
