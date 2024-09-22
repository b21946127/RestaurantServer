using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategoryItems_MenuCategories_MenuCategoryId",
                table: "MenuCategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategoryItems_MenuItems_MenuItemId",
                table: "MenuCategoryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuCategoryItems",
                table: "MenuCategoryItems");

            migrationBuilder.RenameTable(
                name: "MenuCategoryItems",
                newName: "MenuCategoryMenuItems");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategoryItems_MenuItemId",
                table: "MenuCategoryMenuItems",
                newName: "IX_MenuCategoryMenuItems_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuCategoryMenuItems",
                table: "MenuCategoryMenuItems",
                columns: new[] { "MenuCategoryId", "MenuItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategoryMenuItems_MenuCategories_MenuCategoryId",
                table: "MenuCategoryMenuItems",
                column: "MenuCategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategoryMenuItems_MenuItems_MenuItemId",
                table: "MenuCategoryMenuItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategoryMenuItems_MenuCategories_MenuCategoryId",
                table: "MenuCategoryMenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategoryMenuItems_MenuItems_MenuItemId",
                table: "MenuCategoryMenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuCategoryMenuItems",
                table: "MenuCategoryMenuItems");

            migrationBuilder.RenameTable(
                name: "MenuCategoryMenuItems",
                newName: "MenuCategoryItems");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategoryMenuItems_MenuItemId",
                table: "MenuCategoryItems",
                newName: "IX_MenuCategoryItems_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuCategoryItems",
                table: "MenuCategoryItems",
                columns: new[] { "MenuCategoryId", "MenuItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategoryItems_MenuCategories_MenuCategoryId",
                table: "MenuCategoryItems",
                column: "MenuCategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategoryItems_MenuItems_MenuItemId",
                table: "MenuCategoryItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
