using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aspnet_roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                });

            migrationBuilder.CreateTable(
                name: "aspnet_users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    password_hash = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    security_stamp = table.Column<string>(nullable: true),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                        //.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnet_roleclaims_aspnet_roles_roleid",
                        column: x => x.role_id,
                        principalTable: "aspnet_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                        //.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnet_user_claims_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnet_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_aspnet_user_logins_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_aspnet_user_roles_aspnet_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "aspnet_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_aspet_user_roles_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "aspnet_roles",
                column: "normalized_name");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_role_claims_role_id",
                table: "aspnet_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_claims_user_id",
                table: "aspnet_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_logins_user_id",
                table: "aspnet_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_roles_role_id",
                table: "aspnet_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_roles_user_id",
                table: "aspnet_user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "aspnet_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "aspnet_users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnet_role_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_logins");

            migrationBuilder.DropTable(
                name: "aspnet_user_roles");

            migrationBuilder.DropTable(
                name: "aspnet_user_tokens");

            migrationBuilder.DropTable(
                name: "aspnet_roles");

            migrationBuilder.DropTable(
                name: "aspnet_users");
        }
    }
}
