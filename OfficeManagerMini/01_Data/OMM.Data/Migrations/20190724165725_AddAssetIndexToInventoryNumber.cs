using Microsoft.EntityFrameworkCore.Migrations;

namespace OMM.Data.Migrations
{
    public partial class AddAssetIndexToInventoryNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InventoryNumber",
                table: "Assets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_InventoryNumber",
                table: "Assets",
                column: "InventoryNumber",
                unique: true,
                filter: "[InventoryNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_InventoryNumber",
                table: "Assets");

            migrationBuilder.AlterColumn<string>(
                name: "InventoryNumber",
                table: "Assets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
