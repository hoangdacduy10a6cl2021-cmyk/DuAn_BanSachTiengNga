using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingCovers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780143105466-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780142301111-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780192840196-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780156837502-L.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9785040935154-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780525444443-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780199555277-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780156837500-L.jpg");
        }
    }
}
