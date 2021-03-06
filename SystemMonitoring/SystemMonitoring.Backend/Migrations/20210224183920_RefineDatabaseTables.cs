using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SystemMonitoring.Backend.Migrations
{
    public partial class RefineDatabaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestForHangfire");

            migrationBuilder.DropTable(
                name: "TotalJobResults");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "ReoccurringJob");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "CurrentJobResults",
                newName: "ReoccurringJobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReoccurringJobId",
                table: "CurrentJobResults",
                newName: "TaskId");

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "ReoccurringJob",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TestForHangfire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Job = table.Column<string>(type: "text", nullable: true),
                    RunTime = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestForHangfire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TotalJobResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JobType = table.Column<int>(type: "integer", nullable: false),
                    TotalFailedJobs = table.Column<int>(type: "integer", nullable: false),
                    TotalJobs = table.Column<int>(type: "integer", nullable: false),
                    TotalSuccessfulJobs = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalJobResults", x => x.Id);
                });
        }
    }
}
