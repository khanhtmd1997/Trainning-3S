using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class UpdateRoleIdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassWord", "RoleId" },
                values: new object[] { "10000:zRgFnhMtoDaRVX/p8fB65ss6k12HoEcjMRWj6D2LPsw6MLh5", 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PassWord", "RoleId" },
                values: new object[] { "10000:d57BcBDgu8vp4+1vgHDow3fvcmEzN5YE9f18hYuP0U9P7JJv", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "PassWord",
                value: "10000:BCoyg/cq5KDE0uY25VHQdZ+aIw92G4sjxU92BDl1Q5D536qm");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "PassWord",
                value: "10000:vhsavJDSNZpNRtfjyZl9uIVxBOJjx9eLZ2j/0XKrOtWQUGsl");
        }
    }
}
