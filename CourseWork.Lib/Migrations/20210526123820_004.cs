using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Lib.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrajectoryElements_Courses_CourseID",
                table: "TrajectoryElements");

            migrationBuilder.AddForeignKey(
                name: "FK_TrajectoryElements_Courses_CourseID",
                table: "TrajectoryElements",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrajectoryElements_Courses_CourseID",
                table: "TrajectoryElements");

            migrationBuilder.AddForeignKey(
                name: "FK_TrajectoryElements_Courses_CourseID",
                table: "TrajectoryElements",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
