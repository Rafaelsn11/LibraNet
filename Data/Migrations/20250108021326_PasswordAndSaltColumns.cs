using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class PasswordAndSaltColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "tb_users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "tb_users");

            migrationBuilder.DropColumn(
                name: "salt",
                table: "tb_users");
        }
    }
}
