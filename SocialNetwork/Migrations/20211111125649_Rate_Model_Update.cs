using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class Rate_Model_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Rates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoredType",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CommentId",
                table: "Rates",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Comments_CommentId",
                table: "Rates",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Comments_CommentId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_CommentId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "StoredType",
                table: "Rates");
        }
    }
}
