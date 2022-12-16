using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWorkingSolo.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyDataJustCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "28838b5c-1f9b-40b3-90a5-266779ee2c3f", "Category full of different retro gaming consoles: from the gameboy color to the original xbox!", "~/images/retro_gaming_consoles.jpg", "Gaming Consoles" },
                    { "a92c6ebf-67a2-430b-bb70-934d7b533367", "Browse our various antique figurines and action figures!", "~/images/retro_action_figures.jpg", "Figurines" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "28838b5c-1f9b-40b3-90a5-266779ee2c3f");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "a92c6ebf-67a2-430b-bb70-934d7b533367");
        }
    }
}
