using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerECommerce.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFixedSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hashSalt",
                table: "UserCredentials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hashSalt",
                table: "UserCredentials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
