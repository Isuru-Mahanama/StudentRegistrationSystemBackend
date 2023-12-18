using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class createdAddressForeignKey4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_users_studentID",
                table: "addresses",
                column: "studentID",
                principalTable: "users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_users_studentID",
                table: "addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_students_studentID",
                table: "addresses",
                column: "studentID",
                principalTable: "students",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
