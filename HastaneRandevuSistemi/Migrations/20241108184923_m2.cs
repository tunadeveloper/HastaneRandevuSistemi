using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UzmanlikAlani",
                table: "Doktorlar");

            migrationBuilder.AddColumn<int>(
                name: "UzmanlikAlaniId",
                table: "Doktorlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UzmanlikAlanlari",
                columns: table => new
                {
                    UzmanlikAlaniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanlikAlanlari", x => x.UzmanlikAlaniId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_UzmanlikAlaniId",
                table: "Doktorlar",
                column: "UzmanlikAlaniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Doktorlar",
                column: "UzmanlikAlaniId",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "UzmanlikAlaniId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_UzmanlikAlanlari_UzmanlikAlaniId",
                table: "Doktorlar");

            migrationBuilder.DropTable(
                name: "UzmanlikAlanlari");

            migrationBuilder.DropIndex(
                name: "IX_Doktorlar_UzmanlikAlaniId",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "UzmanlikAlaniId",
                table: "Doktorlar");

            migrationBuilder.AddColumn<string>(
                name: "UzmanlikAlani",
                table: "Doktorlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
