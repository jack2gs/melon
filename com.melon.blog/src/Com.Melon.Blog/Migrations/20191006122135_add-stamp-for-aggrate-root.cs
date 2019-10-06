using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Melon.Blog.Migrations
{
    public partial class addstampforaggrateroot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeLastModified",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DateTimeLastModified",
                table: "Posts");
        }
    }
}
