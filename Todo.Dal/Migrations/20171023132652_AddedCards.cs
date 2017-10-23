using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Todo.Dal.Migrations
{
    public partial class AddedCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CardTodoCardId",
                table: "TodoItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    TodoCardId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.TodoCardId);
                    table.ForeignKey(
                        name: "FK_Card_AspNetUsers_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_AspNetUsers_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_CardTodoCardId",
                table: "TodoItem",
                column: "CardTodoCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CreateUserId",
                table: "Card",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_DeleteUserId",
                table: "Card",
                column: "DeleteUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Card_CardTodoCardId",
                table: "TodoItem",
                column: "CardTodoCardId",
                principalTable: "Card",
                principalColumn: "TodoCardId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Card_CardTodoCardId",
                table: "TodoItem");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_CardTodoCardId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "CardTodoCardId",
                table: "TodoItem");
        }
    }
}
