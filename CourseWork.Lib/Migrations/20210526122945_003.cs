using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Lib.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajectories_Specializations_SpecializationID",
                table: "Trajectories");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajectories_Specializations_SpecializationID",
                table: "Trajectories",
                column: "SpecializationID",
                principalTable: "Specializations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trajectories_Specializations_SpecializationID",
                table: "Trajectories");

            migrationBuilder.AddForeignKey(
                name: "FK_Trajectories_Specializations_SpecializationID",
                table: "Trajectories",
                column: "SpecializationID",
                principalTable: "Specializations",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
