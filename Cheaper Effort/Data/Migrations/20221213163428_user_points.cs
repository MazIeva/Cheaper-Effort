using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheaper_Effort.Data.Migrations
{
    public partial class user_points : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPoints",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPoints",
                table: "User");
        }
    }
}
