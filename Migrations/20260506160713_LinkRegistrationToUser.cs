using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoterRegistrationApp.Migrations
{
    /// <inheritdoc />
    public partial class LinkRegistrationToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Registrations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Registrations");
        }
    }
}
