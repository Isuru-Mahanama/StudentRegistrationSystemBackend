using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyCoursesAndSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "CoursesUser",
                columns: table => new
                {
                    coursescourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    usersuserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesUser", x => new { x.coursescourseCode, x.usersuserID });
                    table.ForeignKey(
                        name: "FK_CoursesUser_courses_coursescourseCode",
                        column: x => x.coursescourseCode,
                        principalTable: "courses",
                        principalColumn: "courseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesUser_users_usersuserID",
                        column: x => x.usersuserID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollements_coursCode",
                table: "enrollements",
                column: "coursCode");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesUser_usersuserID",
                table: "CoursesUser",
                column: "usersuserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesUser");

            migrationBuilder.DropIndex(
                name: "IX_enrollements_coursCode",
                table: "enrollements");

            migrationBuilder.CreateIndex(
                name: "IX_enrollements_coursCode",
                table: "enrollements",
                column: "coursCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_enrollements_userID",
                table: "enrollements",
                column: "userID",
                unique: true);
        }
    }
}
