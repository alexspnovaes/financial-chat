using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialChat.Infra.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Message_MessageId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MessageId",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MessageId",
                schema: "dbo",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                schema: "dbo",
                table: "Message",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                schema: "dbo",
                table: "Message",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                schema: "dbo",
                table: "Message",
                column: "SenderId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SenderId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                schema: "dbo",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_MessageId",
                schema: "dbo",
                table: "User",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Message_MessageId",
                schema: "dbo",
                table: "User",
                column: "MessageId",
                principalSchema: "dbo",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
