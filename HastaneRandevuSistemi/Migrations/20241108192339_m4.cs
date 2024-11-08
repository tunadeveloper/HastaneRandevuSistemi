using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "IptalEdildi",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "HastaId",
                table: "Randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DoktorId",
                table: "Randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular",
                column: "HastaId",
                principalTable: "Hastalar",
                principalColumn: "HastaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "HastaId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoktorId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IptalEdildi",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular",
                column: "HastaId",
                principalTable: "Hastalar",
                principalColumn: "HastaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
