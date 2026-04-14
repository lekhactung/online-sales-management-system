using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2b$10$BsLfKnfnrXX70kD1DAIQKuCdvxfjO5Ij75bmE9fNmgNrxK8T5/xYm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "PasswordHash",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 2,
                column: "PasswordHash",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 3,
                column: "PasswordHash",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "AdminAccount",
                keyColumn: "AdminId",
                keyValue: 4,
                column: "PasswordHash",
                value: "123456");
        }
    }
}
