using Microsoft.EntityFrameworkCore.Migrations;

namespace OMM.Data.Migrations
{
    public partial class FixLeavingReasonsTableTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_leavingReasons_LeavingReasonId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leavingReasons",
                table: "leavingReasons");

            migrationBuilder.RenameTable(
                name: "leavingReasons",
                newName: "LeavingReasons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeavingReasons",
                table: "LeavingReasons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LeavingReasons_LeavingReasonId",
                table: "AspNetUsers",
                column: "LeavingReasonId",
                principalTable: "LeavingReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LeavingReasons_LeavingReasonId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeavingReasons",
                table: "LeavingReasons");

            migrationBuilder.RenameTable(
                name: "LeavingReasons",
                newName: "leavingReasons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leavingReasons",
                table: "leavingReasons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_leavingReasons_LeavingReasonId",
                table: "AspNetUsers",
                column: "LeavingReasonId",
                principalTable: "leavingReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
