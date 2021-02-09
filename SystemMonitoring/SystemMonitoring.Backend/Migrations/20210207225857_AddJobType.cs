using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMonitoring.Backend.Migrations
{
    public partial class AddJobType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "JobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "JobResults");
        }
    }
}
