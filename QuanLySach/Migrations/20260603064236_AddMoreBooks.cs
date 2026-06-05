using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLySach.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "CoverImageUrl", "IsNew", "IsPopular", "Price", "Title" },
                values: new object[,]
                {
                    { 7, "Фёдор Достоевский", 1, "/images/book7.jpg", false, true, 620m, "Преступление и наказание" },
                    { 8, "Лев Толстой", 1, "/images/book8.jpg", false, true, 750m, "Война и мир" },
                    { 9, "Лев Толстой", 1, "/images/book9.jpg", false, false, 680m, "Анна Каренина" },
                    { 10, "Александр Дюма", 2, "/images/book10.jpg", false, false, 520m, "Три мушкетёра" },
                    { 11, "Александр Дюма", 2, "/images/book11.jpg", false, true, 580m, "Граф Монте-Кристо" },
                    { 12, "Артур Конан Дойл", 2, "/images/book12.jpg", false, true, 490m, "Шерлок Холмс" },
                    { 13, "Фрэнк Герберт", 3, "/images/book13.jpg", false, true, 720m, "Дюна" },
                    { 14, "Дуглас Адамс", 3, "/images/book14.jpg", false, false, 430m, "Автостопом по Галактике" },
                    { 15, "Айзек Азимов", 3, "/images/book15.jpg", false, false, 560m, "Основание" },
                    { 16, "Дж. Р. Р. Толкин", 3, "/images/book16.jpg", false, true, 800m, "Властелин колец" },
                    { 17, "Уильям Шекспир", 4, "/images/book17.jpg", false, false, 380m, "Ромео и Джульетта" },
                    { 18, "Шарлотта Бронте", 4, "/images/book18.jpg", false, false, 540m, "Джейн Эйр" },
                    { 19, "Маргарет Митчелл", 4, "/images/book19.jpg", false, true, 670m, "Унесённые ветром" },
                    { 20, "Льюис Кэрролл", 5, "/images/book20.jpg", false, true, 350m, "Алиса в стране чудес" },
                    { 21, "Алан Милн", 5, "/images/book21.jpg", false, false, 320m, "Винни-Пух" },
                    { 22, "Памела Трэверс", 5, "/images/book22.jpg", false, false, 400m, "Мэри Поппинс" },
                    { 23, "Наполеон Хилл", 6, "/images/book23.jpg", false, true, 480m, "Думай и богатей" },
                    { 24, "Роберт Кийосаки", 6, "/images/book24.jpg", false, true, 510m, "Богатый папа, бедный папа" },
                    { 25, "Стивен Кови", 6, "/images/book25.jpg", false, false, 590m, "7 навыков высокоэффективных людей" },
                    { 26, "Стивен Хокинг", 8, "/images/book26.jpg", false, true, 620m, "Краткая история времени" },
                    { 27, "Юваль Ной Харари", 7, "/images/book27.jpg", false, true, 650m, "Sapiens" },
                    { 28, "Юваль Ной Харари", 7, "/images/book28.jpg", false, false, 630m, "Homo Deus" },
                    { 29, "Роберт Чалдини", 9, "/images/book29.jpg", false, true, 550m, "Психология влияния" },
                    { 30, "Даниэль Канеман", 9, "/images/book30.jpg", false, false, 580m, "Думай медленно, решай быстро" },
                    { 31, "Александр Пушкин", 10, "/images/book31.jpg", false, false, 390m, "Евгений Онегин" },
                    { 32, "Николай Гоголь", 1, "/images/book32.jpg", false, false, 460m, "Мёртвые души" },
                    { 33, "Фёдор Достоевский", 1, "/images/book33.jpg", false, false, 590m, "Идиот" },
                    { 34, "Фёдор Достоевский", 1, "/images/book34.jpg", false, false, 710m, "Братья Карамазовы" },
                    { 35, "Аркадий и Борис Стругацкие", 3, "/images/book35.jpg", false, false, 470m, "Пикник на обочине" },
                    { 36, "Станислав Лем", 3, "/images/book36.jpg", false, false, 500m, "Солярис" },
                    { 37, "Дж. Д. Сэлинджер", 1, "/images/book37.jpg", false, false, 430m, "Над пропастью во ржи" },
                    { 38, "Фрэнсис Скотт Фицджеральд", 1, "/images/book38.jpg", false, false, 480m, "Великий Гэтсби" },
                    { 39, "Олдос Хаксли", 3, "/images/book39.jpg", false, false, 520m, "О дивный новый мир" },
                    { 40, "Рэй Брэдбери", 3, "/images/book40.jpg", false, false, 490m, "451 градус по Фаренгейту" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40);
        }
    }
}
