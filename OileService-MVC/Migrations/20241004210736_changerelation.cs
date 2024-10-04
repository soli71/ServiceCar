using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class changerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Customers_CustomerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Customers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CarId",
                table: "Customers",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Cars_CarId",
                table: "Customers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Cars_CarId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CarId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Customers_CustomerId",
                table: "Cars",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
