using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaShop1.Migrations
{
    /// <inheritdoc />
    public partial class Orderkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "OrderCode", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderCode",
                table: "OrderDetails",
                column: "OrderCode",
                principalTable: "Orders",
                principalColumn: "OrderCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderCode",
                table: "OrderDetails");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Orders_OrderCode",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");
        }
    }
}
