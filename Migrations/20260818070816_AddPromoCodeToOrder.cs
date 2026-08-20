using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddPromoCodeToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PromoCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 305, DateTimeKind.Local).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(26));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(27));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(29));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(30));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(32));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(34));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(37));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(41));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(42));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(44));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(46));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(51));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(52));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(57));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(59));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(62));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(65));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(70));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(77));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 18, 14, 8, 16, 306, DateTimeKind.Local).AddTicks(79));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PromoCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Orders");

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
    }
}
