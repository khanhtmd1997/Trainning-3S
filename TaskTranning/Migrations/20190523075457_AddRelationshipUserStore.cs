using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddRelationshipUserStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:+RDp9ZcY5PnYQysYE/mQpcKh4mz3N+N8efSTBOcy6d3aMady");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:EnPj7vB1c76cXyF8lroa7d/pn+B4hyZ62JXcFDLNj5P2czWR");

            migrationBuilder.CreateIndex(
                name: "IX_User_StoreId",
                table: "User",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Store_StoreId",
                table: "User",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Store_StoreId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StoreId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:8J8f7DVofeutqUVos3G0sDw4sPW9RTje97EMAdtKzp3VglCl");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:TNiDV9qnB0NNfj7QNsibY9PulzogVF8j9ANK3EUVae3D3VW5");
        }
    }
}
