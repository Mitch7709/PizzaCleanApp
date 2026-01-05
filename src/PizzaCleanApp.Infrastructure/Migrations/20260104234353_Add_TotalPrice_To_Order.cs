using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaCleanApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_TotalPrice_To_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemToppings",
                table: "OrderItemToppings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderItemToppings");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemToppings",
                table: "OrderItemToppings",
                columns: new[] { "OrderItemId", "ToppingId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItemToppings",
                table: "OrderItemToppings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "OrderItemToppings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItemToppings",
                table: "OrderItemToppings",
                column: "Id");
        }
    }
}
