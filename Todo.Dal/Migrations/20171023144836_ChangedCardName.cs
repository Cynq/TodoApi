using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Todo.Dal.Migrations
{
    public partial class ChangedCardName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Card_CardTodoCardId",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_CardTodoCardId",
                table: "TodoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "CardTodoCardId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "TodoCardId",
                table: "Card");

            migrationBuilder.AddColumn<long>(
                name: "CardId",
                table: "TodoItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CardId",
                table: "Card",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_CardId",
                table: "TodoItem",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Card_CardId",
                table: "TodoItem",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_Card_CardId",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_CardId",
                table: "TodoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Card");

            migrationBuilder.AddColumn<long>(
                name: "CardTodoCardId",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TodoCardId",
                table: "Card",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "TodoCardId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_CardTodoCardId",
                table: "TodoItem",
                column: "CardTodoCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_Card_CardTodoCardId",
                table: "TodoItem",
                column: "CardTodoCardId",
                principalTable: "Card",
                principalColumn: "TodoCardId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
