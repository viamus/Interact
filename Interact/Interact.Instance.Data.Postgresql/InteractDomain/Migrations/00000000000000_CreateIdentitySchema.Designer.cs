using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interact.Instance.Data.Postgresql.InteractDomain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("00000000000000_CreateIdentitySchema")]
    partial class CreateIdentitySchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3");
                //.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            { 
                    b.Property<string>("id");

                    b.Property<string>("concurrency_stamp")
                        .IsConcurrencyToken();

                    b.Property<string>("name")
                        .HasAnnotation("max_length", 256);

                    b.Property<string>("normalized_name")
                        .HasAnnotation("max_length", 256);

                    b.HasKey("id");

                    b.HasIndex("normalized_name")
                        .HasName("role_name_index");

                    b.ToTable("aspnet_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("claim_type");

                    b.Property<string>("claim_value");

                    b.Property<string>("role_id")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("role_id");

                    b.ToTable("aspnet_role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("claim_type");

                    b.Property<string>("claim_value");

                    b.Property<string>("user_id")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("aspnet_user_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("login_provider");

                    b.Property<string>("provider_key");

                    b.Property<string>("provider_display_name");

                    b.Property<string>("user_id")
                        .IsRequired();

                    b.HasKey("login_provider", "provider_key");

                    b.HasIndex("user_id");

                    b.ToTable("aspnet_user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("user_id");

                    b.Property<string>("role_id");

                    b.HasKey("user_id", "role_id");

                    b.HasIndex("role_id");

                    b.HasIndex("user_id");

                    b.ToTable("aspnet_user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("user_id");

                    b.Property<string>("login_provider");

                    b.Property<string>("name");

                    b.Property<string>("value");

                    b.HasKey("user_id", "login_provider", "name");

                    b.ToTable("aspnet_user_tokens");
                });

            modelBuilder.Entity("Interact.Instance.Web.Api.Models.ApplicationUser", b =>
                {
                    b.Property<string>("id");

                    b.Property<int>("access_failed_count");

                    b.Property<string>("concurrency_stamp")
                        .IsConcurrencyToken();

                    b.Property<string>("email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("email_confirmed");

                    b.Property<bool>("lockout_enabled");

                    b.Property<DateTimeOffset?>("lockout_end");

                    b.Property<string>("normalized_email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("normalized_user_name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("password_hash");

                    b.Property<string>("phone_number");

                    b.Property<bool>("phone_number_confirmed");

                    b.Property<string>("security_stamp");

                    b.Property<bool>("two_factor_enabled");

                    b.Property<string>("user_name")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("id");

                    b.HasIndex("normalized_email")
                        .HasName("email_index");

                    b.HasIndex("normalized_user_name")
                        .IsUnique()
                        .HasName("user_name_index");

                    b.ToTable("aspnet_users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany("claims")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Interact.Instance.Web.Api.Models.ApplicationUser")
                        .WithMany("claims")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Interact.Instance.Web.Api.Models.ApplicationUser")
                        .WithMany("logins")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany("users")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interact.Instance.Web.Api.Models.ApplicationUser")
                        .WithMany("roles")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
