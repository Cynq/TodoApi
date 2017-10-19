using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Todo.Dal.Migrations
{
    public partial class AddeUsersToTodoItemsMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreateId",
                table: "TodoItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserTodoItem",
                columns: table => new
                {
                    TodoItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTodoItem", x => new { x.TodoItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTodoItem_TodoItem_TodoItemId",
                        column: x => x.TodoItemId,
                        principalTable: "TodoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTodoItem_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_UserCreateId",
                table: "TodoItem",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTodoItem_UserId",
                table: "UserTodoItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_AspNetUsers_UserCreateId",
                table: "TodoItem",
                column: "UserCreateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_AspNetUsers_UserCreateId",
                table: "TodoItem");

            migrationBuilder.DropTable(
                name: "UserTodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_UserCreateId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
