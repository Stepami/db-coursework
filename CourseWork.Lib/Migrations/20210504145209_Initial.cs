using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceDetail_Amount = table.Column<int>(type: "int", nullable: true),
                    PriceDetail_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceDetail_CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceDetail_PriceString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
