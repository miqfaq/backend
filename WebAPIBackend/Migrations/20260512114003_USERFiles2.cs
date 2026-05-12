using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class USERFiles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "UserFiles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFiles_EmployeeId",
                table: "UserFiles",
                newName: "IX_UserFiles_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Users_UserId",
                table: "UserFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_Users_UserId",
                table: "UserFiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFiles",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFiles_UserId",
                table: "UserFiles",
                newName: "IX_UserFiles_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_Employees_EmployeeId",
                table: "UserFiles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
