using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStock.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentGuid",
                        column: x => x.ParentGuid,
                        principalTable: "ProductCategory",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uom",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uom", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Document_Person_CustomerGuid",
                        column: x => x.CustomerGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_Person_VendorGuid",
                        column: x => x.VendorGuid,
                        principalTable: "Person",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UomGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_CategoryGuid",
                        column: x => x.CategoryGuid,
                        principalTable: "ProductCategory",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Uom_UomGuid",
                        column: x => x.UomGuid,
                        principalTable: "Uom",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDetail",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UomGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetail", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Document_DocumentGuid",
                        column: x => x.DocumentGuid,
                        principalTable: "Document",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Product_ProductGuid",
                        column: x => x.ProductGuid,
                        principalTable: "Product",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Uom_UomGuid",
                        column: x => x.UomGuid,
                        principalTable: "Uom",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Warehouse_WarehouseGuid",
                        column: x => x.WarehouseGuid,
                        principalTable: "Warehouse",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductStockLevel",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStockLevel", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ProductStockLevel_Product_ProductGuid",
                        column: x => x.ProductGuid,
                        principalTable: "Product",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStockLevel_Warehouse_WarehouseGuid",
                        column: x => x.WarehouseGuid,
                        principalTable: "Warehouse",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_CustomerGuid",
                table: "Document",
                column: "CustomerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Document_VendorGuid",
                table: "Document",
                column: "VendorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_DocumentGuid",
                table: "DocumentDetail",
                column: "DocumentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_ProductGuid",
                table: "DocumentDetail",
                column: "ProductGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_UomGuid",
                table: "DocumentDetail",
                column: "UomGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentDetail_WarehouseGuid",
                table: "DocumentDetail",
                column: "WarehouseGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryGuid",
                table: "Product",
                column: "CategoryGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UomGuid",
                table: "Product",
                column: "UomGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentGuid",
                table: "ProductCategory",
                column: "ParentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockLevel_ProductGuid",
                table: "ProductStockLevel",
                column: "ProductGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockLevel_WarehouseGuid",
                table: "ProductStockLevel",
                column: "WarehouseGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentDetail");

            migrationBuilder.DropTable(
                name: "ProductStockLevel");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Uom");
        }
    }
}
