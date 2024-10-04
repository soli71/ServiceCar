using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOil8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerServiceDetail_Oil_OilId",
                table: "CustomerServiceDetail");

            migrationBuilder.DropIndex(
                name: "IX_CustomerServiceDetail_OilId",
                table: "CustomerServiceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oil",
                table: "Oil");

            migrationBuilder.DropColumn(
                name: "OilId",
                table: "CustomerServiceDetail");

            migrationBuilder.RenameTable(
                name: "Oil",
                newName: "Oils");

            migrationBuilder.AddColumn<int>(
                name: "OilId",
                table: "CustomerServices",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oils",
                table: "Oils",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_OilId",
                table: "CustomerServices",
                column: "OilId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerServices_Oils_OilId",
                table: "CustomerServices",
                column: "OilId",
                principalTable: "Oils",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerServices_Oils_OilId",
                table: "CustomerServices");

            migrationBuilder.DropIndex(
                name: "IX_CustomerServices_OilId",
                table: "CustomerServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oils",
                table: "Oils");

            migrationBuilder.DropColumn(
                name: "OilId",
                table: "CustomerServices");

            migrationBuilder.RenameTable(
                name: "Oils",
                newName: "Oil");

            migrationBuilder.AddColumn<int>(
                name: "OilId",
                table: "CustomerServiceDetail",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oil",
                table: "Oil",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServiceDetail_OilId",
                table: "CustomerServiceDetail",
                column: "OilId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerServiceDetail_Oil_OilId",
                table: "CustomerServiceDetail",
                column: "OilId",
                principalTable: "Oil",
                principalColumn: "Id");
        }
    }
}
