using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMM.Data.Migrations
{
    public partial class ChangedAssignmentPropertyNameAndAddedEmployeeDataOfBirthProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Assignments",
                newName: "StartingDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "Assignments",
                newName: "CreatedOn");
        }
    }
}
