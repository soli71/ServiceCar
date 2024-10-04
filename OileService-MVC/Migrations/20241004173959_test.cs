using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerServices_Services_ServiceId",
                table: "CustomerServices");

            migrationBuilder.DropIndex(
                name: "IX_CustomerServices_ServiceId",
                table: "CustomerServices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CustomerServices");

            migrationBuilder.CreateTable(
                name: "CustomerServiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerServiceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerServiceDetail_CustomerServices_CustomerServiceId",
                        column: x => x.CustomerServiceId,
                        principalTable: "CustomerServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerServiceDetail_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServiceDetail_CustomerServiceId",
                table: "CustomerServiceDetail",
                column: "CustomerServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServiceDetail_ServiceId",
                table: "CustomerServiceDetail",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerServiceDetail");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CustomerServices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_ServiceId",
                table: "CustomerServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerServices_Services_ServiceId",
                table: "CustomerServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
