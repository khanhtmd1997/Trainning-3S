using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    ListPrice = table.Column<decimal>(nullable: false),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "BrandId", "CategoryId", "ListPrice", "ModelYear", "Picture", "ProductName" },
                values: new object[,]
                {
                    { 1, 1, 1, 154m, 1, "logo.jpg", "Iphone 7" },
                    { 2, 1, 1, 111m, 1, "logo.jpg", "Iphone X" },
                    { 3, 1, 1, 154m, 1, "logo.jpg", "Oppo S10" },
                    { 4, 1, 1, 176m, 1, "logo.jpg", "Samsung S10+" },
                    { 5, 1, 1, 189m, 1, "logo.jpg", "Lumia" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:MSWpM1l3Eb804f7wiu7JR/dlRkn6h+vPz0jqkZHogktwp/4D");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:jmd2Vx6g6tuxBriwAtV656z9fHtV0+XhkI7BQ5zZjAYdzcOB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:NukQkyOreTbkr8w+25Onka3zKHOTtV7/poidk1oHUqrSQN8J");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:MLrezOqnn0WrcGiqazOrP+7o1rUZXJep3gCKbuhzU/SbbsgK");
        }
    }
}
