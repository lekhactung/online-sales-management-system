using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { "TT01", "Chờ xác nhận" },
                    { "TT02", "Đang chuẩn bị hàng" },
                    { "TT03", "Đang giao hàng" },
                    { "TT04", "Đã giao" },
                    { "TT05", "Đã hủy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "StatusId",
                keyValue: "TT01");

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "StatusId",
                keyValue: "TT02");

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "StatusId",
                keyValue: "TT03");

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "StatusId",
                keyValue: "TT04");

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "StatusId",
                keyValue: "TT05");
        }
    }
}
