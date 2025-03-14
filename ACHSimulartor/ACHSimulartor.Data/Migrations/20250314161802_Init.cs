using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACHSimulartor.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    BankAccountBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserShebaNumber);
                });

            migrationBuilder.CreateTable(
                name: "TransferRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserShebaNumber = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ToShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRequests_Users_UserShebaNumber",
                        column: x => x.UserShebaNumber,
                        principalTable: "Users",
                        principalColumn: "UserShebaNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserShebaNumber = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransferRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransferRequests_TransferRequestId",
                        column: x => x.TransferRequestId,
                        principalTable: "TransferRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserShebaNumber",
                        column: x => x.UserShebaNumber,
                        principalTable: "Users",
                        principalColumn: "UserShebaNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransferRequestId",
                table: "Transactions",
                column: "TransferRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserShebaNumber",
                table: "Transactions",
                column: "UserShebaNumber");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_UserShebaNumber",
                table: "TransferRequests",
                column: "UserShebaNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransferRequests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
