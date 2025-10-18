using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyReservation.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Properties",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                newName: "IX_Properties_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_OwnerId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Properties",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                newName: "IX_Properties_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserId",
                table: "Properties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
