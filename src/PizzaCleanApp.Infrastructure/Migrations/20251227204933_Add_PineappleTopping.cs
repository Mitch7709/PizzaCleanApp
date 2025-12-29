using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaCleanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_PineappleTopping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Toppings",
                columns: new[] { "Id", "Calories", "CategoryType", "CreateDate", "IsActive", "LastUpdated", "Name", "Price" },
                values: new object[] { 7L, 10, "Vegetable", new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Pineapple", 1.20m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 7L);
        }
    }
}
