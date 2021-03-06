using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMonitoring.Backend.Migrations
{
    public partial class UpdateCurrentJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awaiting",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Enqueued",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Failed",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Processing",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Scheduled",
                table: "CurrentJobResults");

            migrationBuilder.RenameColumn(
                name: "Succeeded",
                table: "CurrentJobResults",
                newName: "TaskId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CurrentJobResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CurrentJobResults",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CurrentJobResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CurrentJobResults");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "CurrentJobResults",
                newName: "Succeeded");

            migrationBuilder.AddColumn<int>(
                name: "Awaiting",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Enqueued",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Failed",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Processing",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Scheduled",
                table: "CurrentJobResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
