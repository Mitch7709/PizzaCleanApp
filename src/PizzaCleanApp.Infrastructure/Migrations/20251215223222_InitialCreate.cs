using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaCleanApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Crusts",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                Calories = table.Column<int>(type: "int", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Crusts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Status = table.Column<int>(type: "int", nullable: false),
                OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pizzas",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pizzas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Sizes",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sizes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Toppings",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                Calories = table.Column<int>(type: "int", nullable: false),
                CategoryType = table.Column<int>(type: "int", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Toppings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OrderItems",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderId = table.Column<long>(type: "bigint", nullable: false),
                PizzaId = table.Column<long>(type: "bigint", nullable: false),
                SizeId = table.Column<long>(type: "bigint", nullable: false),
                CrustId = table.Column<long>(type: "bigint", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                SubtotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderItems_Crusts_CrustId",
                    column: x => x.CrustId,
                    principalTable: "Crusts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderItems_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_OrderItems_Pizzas_PizzaId",
                    column: x => x.PizzaId,
                    principalTable: "Pizzas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderItems_Sizes_SizeId",
                    column: x => x.SizeId,
                    principalTable: "Sizes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "PizzaToppings",
            columns: table => new
            {
                PizzaId = table.Column<long>(type: "bigint", nullable: false),
                ToppingId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PizzaToppings", x => new { x.PizzaId, x.ToppingId });
                table.ForeignKey(
                    name: "FK_PizzaToppings_Pizzas_PizzaId",
                    column: x => x.PizzaId,
                    principalTable: "Pizzas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PizzaToppings_Toppings_ToppingId",
                    column: x => x.ToppingId,
                    principalTable: "Toppings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "OrderItemToppings",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                ToppingId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItemToppings", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderItemToppings_OrderItems_OrderItemId",
                    column: x => x.OrderItemId,
                    principalTable: "OrderItems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_OrderItemToppings_Toppings_ToppingId",
                    column: x => x.ToppingId,
                    principalTable: "Toppings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "Crusts",
            columns: new[] { "Id", "Calories", "CreateDate", "IsActive", "LastUpdated", "Name" },
            values: new object[,]
            {
                { 1L, 90, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Thin Crust" },
                { 2L, 120, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Original Crust" },
                { 3L, 150, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Thick Crust" },
                { 4L, 200, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Stuffed Crust" }
            });

        migrationBuilder.InsertData(
            table: "Toppings",
            columns: new[] { "Id", "Calories", "CategoryType", "CreateDate", "IsActive", "LastUpdated", "Name", "Price" },
            values: new object[,]
            {
                { 1L, 54, 1, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Pepperoni", 1.50m },
                { 2L, 61, 1, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Meatballs", 2.00m },
                { 3L, 25, 1, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Steak", 1.75m },
                { 4L, 3, 0, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Mushrooms", 1.00m },
                { 5L, 5, 0, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Onions", 0.75m },
                { 6L, 4, 0, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), true, new DateTime(2025, 12, 11, 2, 0, 0, 0, DateTimeKind.Utc), "Green Peppers", 0.80m }
            });

        migrationBuilder.CreateIndex(
            name: "IX_OrderItems_CrustId",
            table: "OrderItems",
            column: "CrustId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderItems_OrderId",
            table: "OrderItems",
            column: "OrderId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderItems_PizzaId",
            table: "OrderItems",
            column: "PizzaId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderItems_SizeId",
            table: "OrderItems",
            column: "SizeId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderItemToppings_OrderItemId_ToppingId",
            table: "OrderItemToppings",
            columns: new[] { "OrderItemId", "ToppingId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_OrderItemToppings_ToppingId",
            table: "OrderItemToppings",
            column: "ToppingId");

        migrationBuilder.CreateIndex(
            name: "IX_PizzaToppings_PizzaId",
            table: "PizzaToppings",
            column: "PizzaId");

        migrationBuilder.CreateIndex(
            name: "IX_PizzaToppings_ToppingId",
            table: "PizzaToppings",
            column: "ToppingId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OrderItemToppings");

        migrationBuilder.DropTable(
            name: "PizzaToppings");

        migrationBuilder.DropTable(
            name: "OrderItems");

        migrationBuilder.DropTable(
            name: "Toppings");

        migrationBuilder.DropTable(
            name: "Crusts");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Pizzas");

        migrationBuilder.DropTable(
            name: "Sizes");
    }
}
