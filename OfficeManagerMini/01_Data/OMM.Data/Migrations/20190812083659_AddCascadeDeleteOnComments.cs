using Microsoft.EntityFrameworkCore.Migrations;

namespace OMM.Data.Migrations
{
    public partial class AddCascadeDeleteOnComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Assignments_AssignmentId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Assignments_AssignmentId",
                table: "Comments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Assignments_AssignmentId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Assignments_AssignmentId",
                table: "Comments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
