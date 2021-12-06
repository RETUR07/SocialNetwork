using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Migrations
{
    public partial class editedblobmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buffer",
                table: "Blobs");

            migrationBuilder.DropColumn(
                name: "Lenth",
                table: "Blobs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Blobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Buffer",
                table: "Blobs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Lenth",
                table: "Blobs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Blobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
