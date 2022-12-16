using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWorkingSolo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImagesInDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "28838b5c-1f9b-40b3-90a5-266779ee2c3f", "Category full of different retro gaming consoles: from the gameboy color to the original xbox!", "~/wwwroot/images/retro_gaming_consoles.jpg", "Gaming Consoles" },
                    { "a92c6ebf-67a2-430b-bb70-934d7b533367", "Browse our various antique figurines and action figures!", "~/wwwroot/images/retro_action_figures.jpg", "Figurines" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "438ee5b7-0dde-4500-8868-a122dcd2a07a",
                column: "ImagePath",
                value: "~/wwwroot/images/gameboyadvancesp_gaming_console.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "53dbaaf5-3b8d-42e1-af12-e10e779e27a9",
                column: "ImagePath",
                value: "~/wwwroot/images/storm_retro_figurine.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "8aa4d1a2-bd54-4f5b-8811-b754e337790f",
                column: "ImagePath",
                value: "~/wwwroot/gameboycolor_gaming_console.jpg");
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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "438ee5b7-0dde-4500-8868-a122dcd2a07a",
                column: "ImagePath",
                value: "~/images/gameboyadvancesp_gaming_console.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "53dbaaf5-3b8d-42e1-af12-e10e779e27a9",
                column: "ImagePath",
                value: "~/images/storm_retro_figurine.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "8aa4d1a2-bd54-4f5b-8811-b754e337790f",
                column: "ImagePath",
                value: "~/gameboycolor_gaming_console.jpg");
        }
    }
}
