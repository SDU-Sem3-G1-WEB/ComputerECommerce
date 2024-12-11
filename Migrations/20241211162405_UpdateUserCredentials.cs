using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerECommerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hashSalt",
                table: "UserCredentials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hashSalt",
                table: "UserCredentials");
        }
    }
}
