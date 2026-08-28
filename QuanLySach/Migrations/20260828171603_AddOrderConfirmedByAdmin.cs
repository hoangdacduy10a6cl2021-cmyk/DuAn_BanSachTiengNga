using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderConfirmedByAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmedByAdminId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmedByAdminName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConfirmedByAdminId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConfirmedByAdminName",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2939));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2940));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2942));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2943));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2944));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2945));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2947));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2949));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2950));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2951));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2954));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2957));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2959));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2961));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2965));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2966));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2967));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2968));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2969));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2971));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2972));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2973));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2974));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2980));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2981));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2982));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 20, 8, 25, 38, 395, DateTimeKind.Local).AddTicks(2983));
        }
    }
}
