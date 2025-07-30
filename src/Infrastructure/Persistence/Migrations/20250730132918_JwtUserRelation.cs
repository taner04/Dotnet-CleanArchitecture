using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JwtUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Jwt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jwt_UserId",
                table: "Jwt",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jwt_User_UserId",
                table: "Jwt",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jwt_User_UserId",
                table: "Jwt");

            migrationBuilder.DropIndex(
                name: "IX_Jwt_UserId",
                table: "Jwt");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jwt");
        }
    }
}
