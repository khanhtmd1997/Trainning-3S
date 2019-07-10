using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Iphone" },
                    { 2, "Oppo" },
                    { 3, "Samsung" },
                    { 4, "Nokia" },
                    { 5, "Hawai" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:ILQ20u8uF4q0MWjoKY9TRjPaw2FIFlv3PNdZTGqEfACVv4ah");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:a2soGsfxpdIC3xG/HYe4Vc43K/QX7MyxWx5zR6OZUWnyFS+W");
        }
    }
}
