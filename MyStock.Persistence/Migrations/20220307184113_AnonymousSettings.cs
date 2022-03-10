using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class AnonymousSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DefaultAnonymousCustomerOnNewSales",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefaultAnonymousVendorOnNewPurchase",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAnonymousCustomerOnNewSales",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultAnonymousVendorOnNewPurchase",
                table: "Settings");
        }
    }
}
