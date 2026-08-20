using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedDateToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(798));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(819));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(821));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(823));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(824));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(826));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(827));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(828));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(830));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(831));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(832));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(874));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(877));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(878));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(879));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(881));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(882));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(884));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(885));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(887));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(889));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(891));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(892));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(893));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(894));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(897));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 17, 14, 41, 50, 761, DateTimeKind.Local).AddTicks(898));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Books");
        }
    }
}
