using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "scratch");

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    Value = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    ErrorHash = table.Column<int>(nullable: true),
                    FullJson = table.Column<string>(nullable: true),
                    Host = table.Column<string>(maxLength: 200, nullable: true),
                    HttpMethod = table.Column<string>(maxLength: 200, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 200, nullable: true),
                    MachineName = table.Column<string>(maxLength: 200, nullable: true),
                    Message = table.Column<string>(maxLength: 200, nullable: true),
                    RequestId = table.Column<Guid>(nullable: true),
                    Source = table.Column<string>(maxLength: 200, nullable: true),
                    StatusCode = table.Column<int>(nullable: true),
                    Type = table.Column<string>(maxLength: 200, nullable: true),
                    Url = table.Column<string>(maxLength: 200, nullable: true),
                    User = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    LastAuthentication = table.Column<DateTime>(nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    LastUserAgent = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    Salt = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    UserName = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "scratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManagerId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    OptionalDate = table.Column<DateTime>(nullable: true),
                    OptionalDateTimeOffset = table.Column<DateTimeOffset>(nullable: true),
                    OptionalDecimal = table.Column<decimal>(nullable: true),
                    OptionalInt = table.Column<int>(nullable: true),
                    RequiredDate = table.Column<DateTime>(nullable: false),
                    RequiredDateTimeOffset = table.Column<DateTimeOffset>(nullable: false),
                    RequiredDecimal = table.Column<decimal>(nullable: false),
                    RequiredInt = table.Column<int>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Members_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "scratch",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlobOfText",
                schema: "scratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlobOfText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlobOfText_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "scratch",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "scratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "scratch",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "scratch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: true),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "scratch",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlobOfText_MemberId",
                schema: "scratch",
                table: "BlobOfText",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ManagerId",
                schema: "scratch",
                table: "Members",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamId",
                schema: "scratch",
                table: "Projects",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_MemberId",
                schema: "scratch",
                table: "Teams",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSettings");

            migrationBuilder.DropTable(
                name: "Exceptions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TestClasses");

            migrationBuilder.DropTable(
                name: "BlobOfText",
                schema: "scratch");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "scratch");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "scratch");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "scratch");
        }
    }
}
