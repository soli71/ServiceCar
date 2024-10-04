using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OilId",
                table: "CustomerServiceDetail",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Oil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oil", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerServiceDetail_Oil_OilId",
                table: "CustomerServiceDetail");

            migrationBuilder.DropTable(
                name: "Oil");

            migrationBuilder.DropIndex(
                name: "IX_CustomerServiceDetail_OilId",
                table: "CustomerServiceDetail");

            migrationBuilder.DropColumn(
                name: "OilId",
                table: "CustomerServiceDetail");
        }
    }
}
