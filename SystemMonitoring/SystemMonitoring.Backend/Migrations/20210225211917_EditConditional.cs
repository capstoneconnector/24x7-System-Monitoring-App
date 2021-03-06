using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemMonitoring.Backend.Migrations
{
    public partial class EditConditional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conditional",
                table: "ReoccurringJob");

            migrationBuilder.AddColumn<string[]>(
                name: "ConditionalExpression",
                table: "ReoccurringJob",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionalExpression",
                table: "ReoccurringJob");

            migrationBuilder.AddColumn<string>(
                name: "Conditional",
                table: "ReoccurringJob",
                type: "text",
                nullable: true);
        }
    }
}
