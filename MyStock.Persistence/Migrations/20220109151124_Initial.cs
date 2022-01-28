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
                    ParentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "Guid", "Name", "ParentGuid" },
                values: new object[,]
                {
                    { new Guid("195cd36c-28ce-4bf1-ad66-e8100fd75f7a"), "мдф", null },
                    { new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20"), "абой", null },
                    { new Guid("35e1bbe7-4f78-46b6-a79d-7f636d073511"), "Пластика", null },
                    { new Guid("37ca6016-94d5-4933-8003-fe8e1bd9e071"), "шпакиловка", null },
                    { new Guid("4e5cfc6b-a7f5-40d4-944e-cf9a4b9effc0"), "мех", null },
                    { new Guid("526160dd-71ef-4026-9157-2d6f79db6dec"), "водоемулсия", null },
                    { new Guid("5d165655-d731-442c-9751-f837f6539af5"), "бочка", null },
                    { new Guid("5d2e1eeb-a1d2-4429-b8a9-98ab55f2db51"), "вагонка", null },
                    { new Guid("7febd40a-efa2-41fb-8c41-a032c8ff3305"), "Лак ", null },
                    { new Guid("89c3023a-176b-41ef-9c05-9c2a76bb983f"), "река", null },
                    { new Guid("9de4c36f-e06b-47ff-8f8b-d59509364863"), "гипсокартон", null },
                    { new Guid("9f424414-f5bc-497d-9d81-703886be19ad"), "краска", null },
                    { new Guid("a5cb5776-9d9c-4d13-b1d6-a1bfaa631c9d"), "столичниса", null },
                    { new Guid("a75f1846-cf64-40e8-8c0e-e3550eb83792"), "наличник", null },
                    { new Guid("ac7adcde-1bb2-4e0b-ac8d-7e329a5d0fb8"), "дсп", null },
                    { new Guid("cc6bd67a-77d5-4a82-b1cf-c2818a38579a"), "хархела", null },
                    { new Guid("d5d42b57-94c0-4a6a-96af-cc9a21353eea"), "тунука", null },
                    { new Guid("e1d60fda-cab0-4d5d-88ee-18350630fd82"), "семент", null },
                    { new Guid("e3d62479-03df-4501-a49f-083e02ed7d0a"), "кафел", null },
                    { new Guid("f2ce64d0-81e2-427e-bc3e-3c5722579b21"), "дар", null }
                });

            migrationBuilder.InsertData(
                table: "Uom",
                columns: new[] { "Guid", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("111f5764-ac38-4e2b-b13e-4500f41fdcb0"), "м2", "", "Квадратный метр" },
                    { new Guid("196ebabe-fc4b-4b5d-88b9-7af5b21ea1e2"), "шт", "", "Штук" },
                    { new Guid("29c5aa39-171e-479c-8fda-0b115ecef5ab"), "Коробка", "", "Коробка" },
                    { new Guid("3dbcac7b-886a-43dd-8192-290ab78cc5a6"), "мм3", "", "Кубический миллиметр" },
                    { new Guid("44b73bea-5aef-41f6-b4f5-c64b6d3320c9"), "км", "", "Километр" },
                    { new Guid("51080816-016c-45b8-bac2-4fd7efcde58d"), "кг", "", "Килограмм" },
                    { new Guid("5148eda5-b74b-4a25-b651-9a867819f286"), "дм3", "", "Кубический дециметр" },
                    { new Guid("6635914e-aed0-43d4-84dc-16914e7a88cf"), "мм", "", "Миллилитр" },
                    { new Guid("741675fb-0264-4993-90dd-45cb1a9e8eaa"), "см", "", "Сантиметр" },
                    { new Guid("7df46711-684a-4d8b-8bd7-8b711b672462"), "см2", "", "Квадратный сантиметр" },
                    { new Guid("9e80460c-d715-4d39-85b3-c1ddefec5eb8"), "дм", "", "Дециметр" },
                    { new Guid("ac0cd37d-ab32-4935-bb2d-889601a6afc8"), "м3", "", "Кубический метр" },
                    { new Guid("be262853-92fb-4a6e-9da2-30fd3cdc2508"), "Ящик", "", "Ящик" },
                    { new Guid("c2259c26-de49-4916-a410-841854e48078"), "л", "", "Литр" },
                    { new Guid("cf40afa4-a975-47f8-965e-bfaaac1c902e"), "мм", "", "Миллиметр" },
                    { new Guid("d2140dfb-7470-43ba-bd71-a3e90cdc0884"), "бтл", "", "Бутылка" },
                    { new Guid("d887cece-58d4-4360-a669-da6b07f945b1"), "см3", "", "Кубический сантиметр" },
                    { new Guid("d8fd1af6-7e42-4a74-8cf0-0c230fe40082"), "г", "", "Грамм" },
                    { new Guid("dd39171c-0a0a-48e7-8f01-cf7641ff8877"), "м", "", "Метр" },
                    { new Guid("f4e1e837-8c25-48f0-b444-2e6ae4996656"), "мм2", "", "Квадратный миллиметр" },
                    { new Guid("f92cdb11-8795-4871-b574-ce171a02e455"), "т", "", "Тонна" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "Guid", "Name", "ParentGuid" },
                values: new object[,]
                {
                    { new Guid("2cd3de67-005d-4584-a42b-ca66a32b504e"), "10м", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("40bfe345-30a1-47aa-b2f5-e5ff1fda4779"), "1м", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("47c1dd90-956f-450b-8061-1548b2da69e0"), "10м кухни", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("4846f838-78b5-418b-8a51-9fabeb78a2d0"), "10м моюши", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("61cc755a-c7ff-4190-ba62-317e87e4db12"), "1м моюши", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("7fadc282-475b-4e2e-9b0d-d0d019243e28"), "1м корея", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("80ce91cb-561c-4687-84db-579fef1e47b9"), "15м", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("abdfa5fe-617d-4e4a-be1a-aa11eef7c8a4"), "10м бумажни", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("c850e26a-ccb2-4522-842a-fd91379fb606"), "75см моюши", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("d9ad5558-e4d0-4275-ac40-68c858f80a71"), "25м сафед", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") },
                    { new Guid("e54d9f66-f8b0-4eb3-858c-ea2519df4559"), "15м моюши", new Guid("2a4a7775-8de3-4aa4-804f-6b45024fef20") }
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
