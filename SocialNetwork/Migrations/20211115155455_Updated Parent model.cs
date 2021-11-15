using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class UpdatedParentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SoftDeleteId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "SoftDeleteId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SoftDeleteId",
                table: "Blobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoftDeleteId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoftDeleteId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoftDeleteId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoftDeleteId",
                table: "Blobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
