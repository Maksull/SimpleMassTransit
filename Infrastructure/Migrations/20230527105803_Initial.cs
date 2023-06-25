using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("251005b9-581c-4fd6-952e-4785977926fb"), "Watersports" },
                    { new Guid("300cd369-ede3-495c-946a-b84267460626"), "Chess" },
                    { new Guid("50bf3be9-4b63-4ecd-8d2d-3a672c5ef3de"), "Football" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("25d3a624-5e29-4aea-a919-bb978c4ec721"), new Guid("50bf3be9-4b63-4ecd-8d2d-3a672c5ef3de"), "Ball", 19.50m },
                    { new Guid("3ad11ab1-4aea-4099-9a2f-ef1b4c6ab387"), new Guid("300cd369-ede3-495c-946a-b84267460626"), "T-shirt", 1200m },
                    { new Guid("3ceac13e-91f6-44b1-85c8-8c9db129c9ec"), new Guid("50bf3be9-4b63-4ecd-8d2d-3a672c5ef3de"), "Corner Flags", 34.95m },
                    { new Guid("3de347c2-6a33-4864-bc6f-49cba0141060"), new Guid("300cd369-ede3-495c-946a-b84267460626"), "Human Chess Board", 75m },
                    { new Guid("a9803f94-7ef4-4efe-88ea-521e98414094"), new Guid("300cd369-ede3-495c-946a-b84267460626"), "Thinking Cap", 16m },
                    { new Guid("b85a084d-1da8-4dc9-8639-2376ceb5eedf"), new Guid("300cd369-ede3-495c-946a-b84267460626"), "Unsteady Chair", 29.95m },
                    { new Guid("d22eac55-7a90-44ae-b842-1cd073254a1a"), new Guid("251005b9-581c-4fd6-952e-4785977926fb"), "Kayak", 275m },
                    { new Guid("dcdaea60-9f88-438b-8088-ef05519a7f5c"), new Guid("251005b9-581c-4fd6-952e-4785977926fb"), "Lifejacket", 48.95m },
                    { new Guid("e6538bb4-edad-429c-a360-fb04559e5baf"), new Guid("50bf3be9-4b63-4ecd-8d2d-3a672c5ef3de"), "Stadium", 79500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
