using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 0);

            migrationBuilder.InsertData(
                table: "Menus",
                column: "DayOfWeek",
                value: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "DayOfWeek",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "Menus",
                column: "DayOfWeek",
                value: 0);
        }
    }
}
