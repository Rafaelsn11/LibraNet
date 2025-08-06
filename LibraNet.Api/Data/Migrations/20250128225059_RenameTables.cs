using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_roles_tb_roles_RoleId",
                table: "tb_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_roles_tb_users_UserId",
                table: "tb_user_roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "tb_user_roles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tb_user_roles",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_user_roles_RoleId",
                table: "tb_user_roles",
                newName: "IX_tb_user_roles_role_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "tb_roles",
                newName: "role_name");

            migrationBuilder.RenameColumn(
                name: "RoleIdentifier",
                table: "tb_roles",
                newName: "role_identifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_identifier",
                table: "tb_roles",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_roles_tb_roles_role_id",
                table: "tb_user_roles",
                column: "role_id",
                principalTable: "tb_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_roles_tb_users_user_id",
                table: "tb_user_roles",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_roles_tb_roles_role_id",
                table: "tb_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_roles_tb_users_user_id",
                table: "tb_user_roles");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "tb_user_roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "tb_user_roles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_user_roles_role_id",
                table: "tb_user_roles",
                newName: "IX_tb_user_roles_RoleId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tb_roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_name",
                table: "tb_roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "role_identifier",
                table: "tb_roles",
                newName: "RoleIdentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleIdentifier",
                table: "tb_roles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_roles_tb_roles_RoleId",
                table: "tb_user_roles",
                column: "RoleId",
                principalTable: "tb_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_roles_tb_users_UserId",
                table: "tb_user_roles",
                column: "UserId",
                principalTable: "tb_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
