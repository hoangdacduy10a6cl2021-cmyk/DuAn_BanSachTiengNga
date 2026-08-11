using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {
                    { 1, "Administrator", "Toàn quyền quản trị hệ thống" },
                    { 2, "Quản lý", "Quản lý sách, danh mục, đơn hàng" },
                    { 3, "Biên tập viên", "Chỉnh sửa nội dung sách, danh mục" },
                    { 4, "Nhân viên", "Xem và xử lý đơn hàng" }
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_RoleId",
                table: "Admins",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Roles_RoleId",
                table: "Admins",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Roles_RoleId",
                table: "Admins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Admins_RoleId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Admins");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}