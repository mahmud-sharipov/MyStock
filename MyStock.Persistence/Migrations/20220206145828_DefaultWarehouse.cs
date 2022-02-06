using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class DefaultWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Guid", "Description", "Name" },
                values: new object[] { new Guid("84657af7-2f27-4efc-8748-d6a9c1bce7ed"), "Склад по умолчанию", "Главний" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "Guid",
                keyValue: new Guid("84657af7-2f27-4efc-8748-d6a9c1bce7ed"));
        }
    }
}
