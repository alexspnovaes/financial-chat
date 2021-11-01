using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialChat.Infra.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                schema: "dbo",
                table: "Message");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatRoomId",
                schema: "dbo",
                table: "Message",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationId",
                schema: "dbo",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatRoom",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatRoomId",
                schema: "dbo",
                table: "Message",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_DestinationId",
                schema: "dbo",
                table: "Message",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_DestinationId",
                schema: "dbo",
                table: "Message",
                column: "DestinationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ChatRoom_ChatRoomId",
                schema: "dbo",
                table: "Message",
                column: "ChatRoomId",
                principalSchema: "dbo",
                principalTable: "ChatRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_DestinationId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_ChatRoom_ChatRoomId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropTable(
                name: "ChatRoom",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatRoomId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_DestinationId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatRoomId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                schema: "dbo",
                table: "Message");

            migrationBuilder.AddColumn<int>(
                name: "Destination",
                schema: "dbo",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
