using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class AnonymousVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGeneral",
                table: "Person",
                newName: "Vendor_IsAnonymous");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Person",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Vendor_IsAnonymous",
                table: "Person",
                newName: "IsGeneral");
        }
    }
}
