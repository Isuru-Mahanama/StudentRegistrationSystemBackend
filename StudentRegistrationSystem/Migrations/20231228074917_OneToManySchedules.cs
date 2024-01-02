using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class OneToManySchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "courseCode",
                table: "schedulecs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_schedulecs_courseCode",
                table: "schedulecs",
                column: "courseCode");

            migrationBuilder.AddForeignKey(
                name: "FK_schedulecs_courses_courseCode",
                table: "schedulecs",
                column: "courseCode",
                principalTable: "courses",
                principalColumn: "courseCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedulecs_courses_courseCode",
                table: "schedulecs");

            migrationBuilder.DropIndex(
                name: "IX_schedulecs_courseCode",
                table: "schedulecs");

            migrationBuilder.AlterColumn<string>(
                name: "courseCode",
                table: "schedulecs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
