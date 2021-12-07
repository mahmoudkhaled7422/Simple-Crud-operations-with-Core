using Microsoft.EntityFrameworkCore.Migrations;

namespace day1.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_deptId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "deptId",
                table: "Students",
                newName: "DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_deptId",
                table: "Students",
                newName: "IX_Students_DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "deptId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "DeptId",
                table: "Students",
                newName: "deptId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DeptId",
                table: "Students",
                newName: "IX_Students_deptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_deptId",
                table: "Students",
                column: "deptId",
                principalTable: "Departments",
                principalColumn: "deptId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
