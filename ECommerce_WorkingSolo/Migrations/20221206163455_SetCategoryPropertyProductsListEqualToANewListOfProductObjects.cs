using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWorkingSolo.Migrations
{
    /// <inheritdoc />
    public partial class SetCategoryPropertyProductsListEqualToANewListOfProductObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Condition", "Description", "ImagePath", "Name", "Price", "Rating" },
                values: new object[] { 20, 8, 1, "This is a mint condition Gameboy Advance SP Gaming console. Sold with no cartridges", "/images/gaming_consoles/gameboyadvancesp_gaming_console.jpg", "Gameboy Advance Sp", 176.82m, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Condition", "Description", "ImagePath", "Name", "Price", "Rating" },
                values: new object[] { 88, 8, 1, "This is a mint condition Gameboy Advance SP Gaming console. Sold with no cartridges", "/images/gaming_consoles/gameboyadvancesp_gaming_console.jpg", "Gameboy Advance Sp", 176.82m, 4 });
        }
    }
}
