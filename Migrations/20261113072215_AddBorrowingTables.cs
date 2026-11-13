using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookl_Manager_System.Migrations
{
    /// <inheritdoc />
    public partial class AddBorrowingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableStock",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStock",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    BorrowTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReturnTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowOrders_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BorrowOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowOrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowOrderItems_BorrowOrders_BorrowOrderId",
                        column: x => x.BorrowOrderId,
                        principalTable: "BorrowOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowOrderItems_BookId",
                table: "BorrowOrderItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowOrderItems_BorrowOrderId",
                table: "BorrowOrderItems",
                column: "BorrowOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowOrders_StudentId",
                table: "BorrowOrders",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowOrderItems");

            migrationBuilder.DropTable(
                name: "BorrowOrders");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropColumn(
                name: "AvailableStock",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TotalStock",
                table: "Books");
        }
    }
}
