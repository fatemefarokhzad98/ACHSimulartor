using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACHSimulartor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountUsers",
                columns: table => new
                {
                    ShebaNumber = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    AccountBalanc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Transaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUsers", x => x.ShebaNumber);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequests_FromShebaNumber",
                table: "TransferRequests",
                column: "FromShebaNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequests_AccountUsers_FromShebaNumber",
                table: "TransferRequests",
                column: "FromShebaNumber",
                principalTable: "AccountUsers",
                principalColumn: "ShebaNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequests_AccountUsers_FromShebaNumber",
                table: "TransferRequests");

            migrationBuilder.DropTable(
                name: "AccountUsers");

            migrationBuilder.DropIndex(
                name: "IX_TransferRequests_FromShebaNumber",
                table: "TransferRequests");
        }
    }
}
