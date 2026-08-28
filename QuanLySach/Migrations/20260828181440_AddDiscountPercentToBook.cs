using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPercentToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3441), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3460), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3461), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3462), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3464), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3465), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3466), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3467), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3468), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3469), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3471), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3472), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3473), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3474), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3475), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3476), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3477), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3479), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3480), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3481), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3482), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3483), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3485), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3518), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3519), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3520), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3521), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3523), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3524), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3525), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3526), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3527), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3528), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3530), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3531), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3532), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3533), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3534), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3536), 0 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedDate", "DiscountPercent" },
                values: new object[] { new DateTime(2026, 8, 29, 1, 14, 39, 778, DateTimeKind.Local).AddTicks(3537), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6076));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6101));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6103));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6105));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6106));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6108));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6109));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6111));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6113));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6114));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6115));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6116));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6118));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6119));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6121));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6122));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6123));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6124));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6125));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6126));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6127));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6128));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6132));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6165));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6167));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6169));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6171));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6172));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6173));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 29, 0, 16, 2, 758, DateTimeKind.Local).AddTicks(6174));
        }
    }
}
