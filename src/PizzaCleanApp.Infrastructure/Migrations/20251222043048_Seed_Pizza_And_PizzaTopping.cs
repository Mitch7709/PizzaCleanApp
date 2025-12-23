using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaCleanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Pizza_And_PizzaTopping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "BasePrice", "CreateDate", "Description", "IsActive", "LastUpdated", "Name" },
                values: new object[,]
                {
                    { 1L, 8.00m, new DateTime(2025, 12, 31, 2, 0, 0, 0, DateTimeKind.Utc), "Classic pizza with pepperoni slices.", true, new DateTime(2025, 12, 31, 2, 0, 0, 0, DateTimeKind.Utc), "Pepperoni" },
                    { 2L, 12.50m, new DateTime(2025, 12, 31, 2, 0, 0, 0, DateTimeKind.Utc), "Deluxe pizza with a variety of toppings.", true, new DateTime(2025, 12, 31, 2, 0, 0, 0, DateTimeKind.Utc), "Supreme" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "CreateDate", "LastUpdated", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), "Small", 8.00m },
                    { 2L, new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), "Medium", 10.00m },
                    { 3L, new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 21, 2, 0, 0, 0, DateTimeKind.Utc), "Large", 12.00m }
                });

            migrationBuilder.InsertData(
                table: "PizzaToppings",
                columns: new[] { "PizzaId", "ToppingId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 2L, 2L },
                    { 2L, 4L },
                    { 2L, 5L },
                    { 2L, 6L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2L, 4L });

            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2L, 5L });

            migrationBuilder.DeleteData(
                table: "PizzaToppings",
                keyColumns: new[] { "PizzaId", "ToppingId" },
                keyValues: new object[] { 2L, 6L });

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
