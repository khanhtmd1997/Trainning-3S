using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreName = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "Id", "City", "Email", "Phone", "State", "StoreName", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Hue City", "khanhtmd99@gmail.com", 364606406, "", "KhanhStore", "18/11 Thuy Tu", "49000" },
                    { 2, "Hue City", "Loc97@gmail.com", 364606406, "", "LocStore", "18/11 Thuy Tu", "49000" },
                    { 3, "Hue City", "Huan97@gmail.com", 364606406, "", "HuanStore", "18/11 Thuy Tu", "49000" },
                    { 4, "Hue City", "Tuan97@gmail.com", 364606406, "", "TuanStore", "18/11 Thuy Tu", "49000" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:NmhiNuWZVWqp4PFYlC9V8hfOXAqDa+xuHdGBpfAwYHLTre/w");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:UIG0luYxLNx5HpwbfiLtaHHJbZIgW/v8IbLPS/pOmIbM+E8b");
        }
    }
}
