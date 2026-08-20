using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class FixCovers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780142301111-L.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "CoverImageUrl",
                value: "https://covers.openlibrary.org/b/isbn/9780156837502-L.jpg");
        }
    }
}
