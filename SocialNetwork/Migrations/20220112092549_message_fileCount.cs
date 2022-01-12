using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class message_fileCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Posts_PostId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_PostId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileCount",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileCount",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Rates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_PostId",
                table: "Rates",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Posts_PostId",
                table: "Rates",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
