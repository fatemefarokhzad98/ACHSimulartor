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
                name: "AccountUsers",
                columns: table => new
                {
                    ShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Transaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUsers", x => x.ShebaNumber);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankCode = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankCode);
                });

            migrationBuilder.CreateTable(
                name: "TransferRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ToShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Staus = table.Column<int>(type: "int", nullable: true),
                    BankCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRequests_AccountUsers_FromShebaNumber",
                        column: x => x.FromShebaNumber,
                        principalTable: "AccountUsers",
                        principalColumn: "ShebaNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferRequests_Banks_BankCode",
                        column: x => x.BankCode,
                        principalTable: "Banks",
                        principalColumn: "BankCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_BankCode",
                table: "TransferRequests",
                column: "BankCode");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_FromShebaNumber",
                table: "TransferRequests",
                column: "FromShebaNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferRequests");

            migrationBuilder.DropTable(
                name: "AccountUsers");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
