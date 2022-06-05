using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQL_Provider.Migrations
{
    public partial class UsersTableUpadte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "UserID");
        }
    }
}
