using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaCleanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Calories_ToSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Sizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Calories",
                value: 186);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Calories",
                value: 220);

            migrationBuilder.UpdateData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Calories",
                value: 286);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Sizes");
        }
    }
}
