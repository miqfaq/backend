using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIBackend.Migrations
{
    /// <inheritdoc />
    public partial class DTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTime_Tools_ToolId",
                table: "WorkTime");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "WorkTime",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Tools",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "workTimeId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_DepartmentId",
                table: "Tools",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_workTimeId",
                table: "Employees",
                column: "workTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_WorkTime_workTimeId",
                table: "Employees",
                column: "workTimeId",
                principalTable: "WorkTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Departments_DepartmentId",
                table: "Tools",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTime_Tools_ToolId",
                table: "WorkTime",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_WorkTime_workTimeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Departments_DepartmentId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTime_Tools_ToolId",
                table: "WorkTime");

            migrationBuilder.DropIndex(
                name: "IX_Tools_DepartmentId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Employees_workTimeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "workTimeId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ToolId",
                table: "WorkTime",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTime_Tools_ToolId",
                table: "WorkTime",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
