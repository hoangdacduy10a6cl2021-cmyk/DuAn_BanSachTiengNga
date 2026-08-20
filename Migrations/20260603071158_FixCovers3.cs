using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class FixCovers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CoverImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/1/10/WinniePooh.png");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CoverImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/en/0/0e/Solaris_cover.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780140301656-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780571057142-L.jpg");
        }
    }
}
