using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddContactMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    AdminReply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepliedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReplyRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropTable(
                name: "ContactMessages");
        }
    }
}
