using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWorkingSolo.Migrations
{
    /// <inheritdoc />
    public partial class CreatedUserBillingModelClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "28838b5c-1f9b-40b3-90a5-266779ee2c3f");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "a92c6ebf-67a2-430b-bb70-934d7b533367");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "438ee5b7-0dde-4500-8868-a122dcd2a07a");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "53dbaaf5-3b8d-42e1-af12-e10e779e27a9");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "8aa4d1a2-bd54-4f5b-8811-b754e337790f");

            migrationBuilder.CreateTable(
                name: "UserBillingInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsersOGID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBillingInfo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBillingInfo");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "28838b5c-1f9b-40b3-90a5-266779ee2c3f", "Category full of different retro gaming consoles: from the gameboy color to the original xbox!", "./wwwroot/images/retro_gaming_consoles.jpg", "Gaming Consoles" },
                    { "a92c6ebf-67a2-430b-bb70-934d7b533367", "Browse our various antique figurines and action figures!", "./wwwroot/images/retro_action_figures.jpg", "Figurines" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Condition", "Description", "ImagePath", "Name", "Price", "Rating", "ShoppingCartId" },
                values: new object[,]
                {
                    { "438ee5b7-0dde-4500-8868-a122dcd2a07a", "28838b5c-1f9b-40b3-90a5-266779ee2c3f", 3, "Used Gameboy Advance Sp", "./wwwroot/images/gameboyadvancesp_gaming_console.jpg", "Gameboy Advance Sp", 150.00m, 3, null },
                    { "53dbaaf5-3b8d-42e1-af12-e10e779e27a9", "a92c6ebf-67a2-430b-bb70-934d7b533367", 5, "Mint condition Storm figurine from the X-Men comic series", "./wwwroot/images/storm_retro_figurine.jpg", "Storm X-Men Figurine", 300.00m, 5, null },
                    { "8aa4d1a2-bd54-4f5b-8811-b754e337790f", "28838b5c-1f9b-40b3-90a5-266779ee2c3f", 3, "Gently used Gameboy Color!", "./wwwroot/gameboycolor_gaming_console.jpg", "Gameboy Color", 250.00m, 4, null }
                });
        }
    }
}
