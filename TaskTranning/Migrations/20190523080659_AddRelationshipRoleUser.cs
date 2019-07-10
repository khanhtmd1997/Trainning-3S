using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddRelationshipRoleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:hzAqdlm7tCsBbs7qMr0JHz8EtcaggsWMXeDip6wr0/RIBtOj");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:p0NYL/D5zot90lPzCfR1831h3OgS57zLv+MdrLhCsLEfQECO");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:zRgFnhMtoDaRVX/p8fB65ss6k12HoEcjMRWj6D2LPsw6MLh5");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:d57BcBDgu8vp4+1vgHDow3fvcmEzN5YE9f18hYuP0U9P7JJv");
        }
    }
}
