using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Management_Data.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActivityDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 5, 11, 53, 183, DateTimeKind.Utc).AddTicks(6616),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 1, 33, 40, 851, DateTimeKind.Utc).AddTicks(9531));

            migrationBuilder.AlterColumn<string>(
                name: "HireDate",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "22.01.2024 05:11:53",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "22.01.2024 01:33:40");

            migrationBuilder.AlterColumn<int>(
                name: "RequesterUserID",
                schema: "dbo",
                table: "Reports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 5, 11, 53, 184, DateTimeKind.Utc).AddTicks(7827),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 1, 33, 40, 853, DateTimeKind.Utc).AddTicks(118));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserNumber",
                keyValue: 1,
                columns: new[] { "HireDate", "LastActivityDate" },
                values: new object[] { "22.01.2024 05:11:53", new DateTime(2024, 1, 22, 5, 11, 53, 183, DateTimeKind.Utc).AddTicks(6616) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserNumber",
                keyValue: 2,
                columns: new[] { "HireDate", "LastActivityDate" },
                values: new object[] { "22.01.2024 05:11:53", new DateTime(2024, 1, 22, 5, 11, 53, 183, DateTimeKind.Utc).AddTicks(6616) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActivityDate",
                schema: "dbo",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 1, 33, 40, 851, DateTimeKind.Utc).AddTicks(9531),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 5, 11, 53, 183, DateTimeKind.Utc).AddTicks(6616));

            migrationBuilder.AlterColumn<string>(
                name: "HireDate",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "22.01.2024 01:33:40",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "22.01.2024 05:11:53");

            migrationBuilder.AlterColumn<int>(
                name: "RequesterUserID",
                schema: "dbo",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 22, 1, 33, 40, 853, DateTimeKind.Utc).AddTicks(118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 22, 5, 11, 53, 184, DateTimeKind.Utc).AddTicks(7827));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserNumber",
                keyValue: 1,
                columns: new[] { "HireDate", "LastActivityDate" },
                values: new object[] { "22.01.2024 01:33:40", new DateTime(2024, 1, 22, 1, 33, 40, 851, DateTimeKind.Utc).AddTicks(9531) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "UserNumber",
                keyValue: 2,
                columns: new[] { "HireDate", "LastActivityDate" },
                values: new object[] { "22.01.2024 01:33:40", new DateTime(2024, 1, 22, 1, 33, 40, 851, DateTimeKind.Utc).AddTicks(9531) });
        }
    }
}
