using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class DocNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextPurchaseDocNumber",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NextSalesDocNumber",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextPurchaseDocNumber",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "NextSalesDocNumber",
                table: "Settings");
        }
    }
}
