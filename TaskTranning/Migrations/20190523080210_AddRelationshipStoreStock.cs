using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddRelationshipStoreStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:zDo9OGTMC1l9SNiFYBTq++S9ZGCE5L43e8C1NLEUaw/SBmw4");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:HRNkD1WSS351ZCqxFT/NUB7SHIrZ7exWNgj2KcUkVBJ339v6");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StoreId",
                table: "Stock",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Store_StoreId",
                table: "Stock",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Store_StoreId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_StoreId",
                table: "Stock");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:uSpD3pz/hUTlRRx+dQ0V5jgcJW+sXirCnNrl1wn0mUxYp5hb");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:6VxjXal4jxXio3cswOa4YeEk4F3YQj3ghJvct0SAx3iS5o5g");
        }
    }
}
