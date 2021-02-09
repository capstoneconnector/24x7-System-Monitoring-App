using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SystemMonitoring.Backend.Migrations
{
    public partial class AddCurrentJobResultsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobResults",
                table: "JobResults");

            migrationBuilder.RenameTable(
                name: "JobResults",
                newName: "TotalJobResults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalJobResults",
                table: "TotalJobResults",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurrentJobResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Enqueued = table.Column<int>(type: "integer", nullable: false),
                    Scheduled = table.Column<int>(type: "integer", nullable: false),
                    Processing = table.Column<int>(type: "integer", nullable: false),
                    Succeeded = table.Column<int>(type: "integer", nullable: false),
                    Failed = table.Column<int>(type: "integer", nullable: false),
                    Deleted = table.Column<int>(type: "integer", nullable: false),
                    Awaiting = table.Column<int>(type: "integer", nullable: false),
                    JobType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentJobResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentJobResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalJobResults",
                table: "TotalJobResults");

            migrationBuilder.RenameTable(
                name: "TotalJobResults",
                newName: "JobResults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobResults",
                table: "JobResults",
                column: "Id");
        }
    }
}
