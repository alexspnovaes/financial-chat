using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialChat.Infra.Data.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Names",
                schema: "dbo",
                table: "ChatRoom",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "ChatRoom",
                newName: "Names");
        }
    }
}
