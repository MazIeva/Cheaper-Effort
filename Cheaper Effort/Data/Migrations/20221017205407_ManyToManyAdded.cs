using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheaper_Effort.Data.Migrations
{
    public partial class ManyToManyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Ingredients_IngredientId",
                table: "Recipe_Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Ingredients_RecipeId",
                table: "Recipe_Ingredients",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe_Ingredients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Recipes",
                type: "TEXT",
                maxLength: 400,
                nullable: false,
                defaultValue: "");
        }
    }
}
