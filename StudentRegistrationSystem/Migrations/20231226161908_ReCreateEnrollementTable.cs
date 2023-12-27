using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ReCreateEnrollementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enrollements",
                columns: table => new
                {
                    coursCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    enrollementStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollements", x => new { x.userID, x.coursCode });
                    table.ForeignKey(
                        name: "FK_enrollements_courses_coursCode",
                        column: x => x.coursCode,
                        principalTable: "courses",
                        principalColumn: "courseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_enrollements_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "enrollements");
        }
    }
}
