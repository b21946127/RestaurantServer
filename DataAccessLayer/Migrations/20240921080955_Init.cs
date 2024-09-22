using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_Menus_MenuId",
                table: "MenuCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "MenuCategories",
                newName: "MenuDayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_MenuId",
                table: "MenuCategories",
                newName: "IX_MenuCategories_MenuDayOfWeek");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "DayOfWeek");

            migrationBuilder.InsertData(
                table: "Menus",
                column: "DayOfWeek",
                values: new object[]
                {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_Menus_MenuDayOfWeek",
                table: "MenuCategories",
                column: "MenuDayOfWeek",
                principalTable: "Menus",
                principalColumn: "DayOfWeek",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_Menus_MenuDayOfWeek",
                table: "MenuCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "MenuDayOfWeek",
                table: "MenuCategories",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_MenuDayOfWeek",
                table: "MenuCategories",
                newName: "IX_MenuCategories_MenuId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Menus",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_Menus_MenuId",
                table: "MenuCategories",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
