using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaShop1.Migrations
{
    /// <inheritdoc />
    public partial class TestRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order_Code",
                table: "Orders",
                newName: "OrderCode");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "OrderDetails",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderCode",
                table: "Orders",
                newName: "Order_Code");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "OrderDetails",
                newName: "UserName");
        }
    }
}
