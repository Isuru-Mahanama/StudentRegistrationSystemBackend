using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class sharedprimarykey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                   name: "studentID",
                   table: "students",
                   type: "int",
                   nullable: false);
          

            migrationBuilder.AddForeignKey(
                name: "FK_students_users_studentID",
                table: "students",
                column: "studentID",
                principalTable: "users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_users_studentID",
                table: "students");

            migrationBuilder.DropColumn(
                name: "studentID",
                table: "students");

        }
    }
}
