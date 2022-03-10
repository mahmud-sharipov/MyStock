using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class PaidStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFullyPaid",
                table: "Document",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFullyPaid",
                table: "Document");
        }
    }
}
