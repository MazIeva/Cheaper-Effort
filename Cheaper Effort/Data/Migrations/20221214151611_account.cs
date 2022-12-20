using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheaper_Effort.Data.Migrations
{
    public partial class account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "User",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "User");
        }
    }
}
