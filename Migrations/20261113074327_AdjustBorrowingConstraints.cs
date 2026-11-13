using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookl_Manager_System.Migrations
{
    /// <inheritdoc />
    public partial class AdjustBorrowingConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrderItems_Books_BookId",
                table: "BorrowOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrderItems_BorrowOrders_BorrowOrderId",
                table: "BorrowOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrders_Students_StudentId",
                table: "BorrowOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnTime",
                table: "BorrowOrders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueTime",
                table: "BorrowOrders",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrderItems_Books_BookId",
                table: "BorrowOrderItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrderItems_BorrowOrders_BorrowOrderId",
                table: "BorrowOrderItems",
                column: "BorrowOrderId",
                principalTable: "BorrowOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrders_Students_StudentId",
                table: "BorrowOrders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrderItems_Books_BookId",
                table: "BorrowOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrderItems_BorrowOrders_BorrowOrderId",
                table: "BorrowOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowOrders_Students_StudentId",
                table: "BorrowOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnTime",
                table: "BorrowOrders",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueTime",
                table: "BorrowOrders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrderItems_Books_BookId",
                table: "BorrowOrderItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrderItems_BorrowOrders_BorrowOrderId",
                table: "BorrowOrderItems",
                column: "BorrowOrderId",
                principalTable: "BorrowOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowOrders_Students_StudentId",
                table: "BorrowOrders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
