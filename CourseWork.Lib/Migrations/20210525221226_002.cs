using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Lib.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trajectories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trajectories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trajectories_Specializations_SpecializationID",
                        column: x => x.SpecializationID,
                        principalTable: "Specializations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Trajectories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrajectoryElements",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TrajectoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Passed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrajectoryElements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrajectoryElements_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TrajectoryElements_Trajectories_TrajectoryID",
                        column: x => x.TrajectoryID,
                        principalTable: "Trajectories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trajectories_SpecializationID",
                table: "Trajectories",
                column: "SpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trajectories_UserID",
                table: "Trajectories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TrajectoryElements_CourseID",
                table: "TrajectoryElements",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TrajectoryElements_TrajectoryID",
                table: "TrajectoryElements",
                column: "TrajectoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrajectoryElements");

            migrationBuilder.DropTable(
                name: "Trajectories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
