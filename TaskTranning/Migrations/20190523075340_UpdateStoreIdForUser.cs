using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class UpdateStoreIdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassWord", "StoreId" },
                values: new object[] { "10000:8J8f7DVofeutqUVos3G0sDw4sPW9RTje97EMAdtKzp3VglCl", 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PassWord", "StoreId" },
                values: new object[] { "10000:TNiDV9qnB0NNfj7QNsibY9PulzogVF8j9ANK3EUVae3D3VW5", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:bBX3LwFCJ02FfAbkkdlGT8u0OFdycP96+xmVNj0zuYhHSHPr");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:qUI/b0pdwKOM18Q79AtBlnXtf3IY6rNa25KMPTLxyWAbJRYR");
        }
    }
}
