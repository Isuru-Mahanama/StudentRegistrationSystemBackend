using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "users",
               columns: table => new
               {
                   email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   userID = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1")
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_users", x => x.email);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "users");
        }
    }
}
