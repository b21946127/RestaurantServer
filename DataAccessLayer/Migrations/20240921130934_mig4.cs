using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOption_MenuItems_MenuItemId",
                table: "MenuItemOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOption",
                table: "MenuItemOption");

            migrationBuilder.RenameTable(
                name: "MenuItemOption",
                newName: "MenuItemOptions");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOption_MenuItemId",
                table: "MenuItemOptions",
                newName: "IX_MenuItemOptions_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOptions",
                table: "MenuItemOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptions_MenuItems_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptions_MenuItems_MenuItemId",
                table: "MenuItemOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemOptions",
                table: "MenuItemOptions");

            migrationBuilder.RenameTable(
                name: "MenuItemOptions",
                newName: "MenuItemOption");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOption",
                newName: "IX_MenuItemOption_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemOption",
                table: "MenuItemOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOption_MenuItems_MenuItemId",
                table: "MenuItemOption",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
