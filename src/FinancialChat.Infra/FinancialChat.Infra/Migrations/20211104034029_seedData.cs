using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialChat.Infra.Data.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ChatRoom",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("24e42333-b862-49ff-bb48-980ad6a66b48"), "Financial Chat Room 1" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ChatRoom",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("2f8c84ce-f8ce-405e-ab0d-c068d400664c"), "Financial Chat Room 2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ChatRoom",
                keyColumn: "Id",
                keyValue: new Guid("24e42333-b862-49ff-bb48-980ad6a66b48"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ChatRoom",
                keyColumn: "Id",
                keyValue: new Guid("2f8c84ce-f8ce-405e-ab0d-c068d400664c"));
        }
    }
}
