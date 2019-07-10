using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTranning.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Email", "FullName", "IsActive", "PassWord", "Phone", "Picture" },
                values: new object[] { 1, "18/11 Thuy Tu", "khanhtmd97@gmail.com", "Nguyen Van Khanh", true, "10000:NmhiNuWZVWqp4PFYlC9V8hfOXAqDa+xuHdGBpfAwYHLTre/w", 364606406, "logo.jpg" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Email", "FullName", "IsActive", "PassWord", "Phone", "Picture" },
                values: new object[] { 2, "25/23/131 Tran Phu", "hoaithu1486@gmail.com", "Tran Thi Hoai Thu", true, "10000:UIG0luYxLNx5HpwbfiLtaHHJbZIgW/v8IbLPS/pOmIbM+E8b", 364606406, "logo.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
