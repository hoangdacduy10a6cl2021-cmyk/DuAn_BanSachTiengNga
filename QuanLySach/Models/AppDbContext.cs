using Microsoft.EntityFrameworkCore;

namespace QuanLySach.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<PaymentCard> PaymentCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Художественная литература" },
                new Category { Id = 2, Name = "Детективы" },
                new Category { Id = 3, Name = "Фантастика" },
                new Category { Id = 4, Name = "Романтика" },
                new Category { Id = 5, Name = "Детская литература" },
                new Category { Id = 6, Name = "Бизнес и саморазвитие" },
                new Category { Id = 7, Name = "История" },
                new Category { Id = 8, Name = "Наука и техника" },
                new Category { Id = 9, Name = "Психология" },
                new Category { Id = 10, Name = "Поэзия" },
                new Category { Id = 11, Name = "Кулинария" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Маленький принц", Author = "Антуан де Сент-Экзюпери", Price = 450, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780156012195-L.jpg", CategoryId = 1, IsPopular = true },
                new Book { Id = 2, Title = "1984", Author = "Джордж Оруэлл", Price = 550, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780451524935-L.jpg", CategoryId = 2, IsPopular = true },
                new Book { Id = 3, Title = "Гордость и предубеждение", Author = "Джейн Остин", Price = 600, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141439518-L.jpg", CategoryId = 4, IsPopular = true },
                new Book { Id = 4, Title = "Тихий Дон", Author = "Михаил Шолохов", Price = 700, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780143105466-L.jpg", CategoryId = 1, IsPopular = true },
                new Book { Id = 5, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Price = 650, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141180144-L.jpg", CategoryId = 1, IsPopular = true },
                new Book { Id = 6, Title = "Гарри Поттер и Философский камень", Author = "Дж. К. Роулинг", Price = 500, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780439708180-L.jpg", CategoryId = 3, IsPopular = true },
                new Book { Id = 7, Title = "Преступление и наказание", Author = "Фёдор Достоевский", Price = 620, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780143058144-L.jpg", CategoryId = 1, IsPopular = true },
                new Book { Id = 8, Title = "Война и мир", Author = "Лев Толстой", Price = 750, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781400079988-L.jpg", CategoryId = 1, IsPopular = true },
                new Book { Id = 9, Title = "Анна Каренина", Author = "Лев Толстой", Price = 680, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780143035008-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 10, Title = "Три мушкетёра", Author = "Александр Дюма", Price = 520, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780140440737-L.jpg", CategoryId = 2, IsPopular = false },
                new Book { Id = 11, Title = "Граф Монте-Кристо", Author = "Александр Дюма", Price = 580, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780140449266-L.jpg", CategoryId = 2, IsPopular = true },
                new Book { Id = 12, Title = "Шерлок Холмс", Author = "Артур Конан Дойл", Price = 490, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780307269751-L.jpg", CategoryId = 2, IsPopular = true },
                new Book { Id = 13, Title = "Дюна", Author = "Фрэнк Герберт", Price = 720, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780441013593-L.jpg", CategoryId = 3, IsPopular = true },
                new Book { Id = 14, Title = "Автостопом по Галактике", Author = "Дуглас Адамс", Price = 430, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780345391803-L.jpg", CategoryId = 3, IsPopular = false },
                new Book { Id = 15, Title = "Основание", Author = "Айзек Азимов", Price = 560, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553293357-L.jpg", CategoryId = 3, IsPopular = false },
                new Book { Id = 16, Title = "Властелин колец", Author = "Дж. Р. Р. Толкин", Price = 800, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780618640157-L.jpg", CategoryId = 3, IsPopular = true },
                new Book { Id = 17, Title = "Ромео и Джульетта", Author = "Уильям Шекспир", Price = 380, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780743477116-L.jpg", CategoryId = 4, IsPopular = false },
                new Book { Id = 18, Title = "Джейн Эйр", Author = "Шарлотта Бронте", Price = 540, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141441146-L.jpg", CategoryId = 4, IsPopular = false },
                new Book { Id = 19, Title = "Унесённые ветром", Author = "Маргарет Митчелл", Price = 670, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781416548898-L.jpg", CategoryId = 4, IsPopular = true },
                new Book { Id = 20, Title = "Алиса в стране чудес", Author = "Льюис Кэрролл", Price = 350, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780141439761-L.jpg", CategoryId = 5, IsPopular = true },
                new Book { Id = 21, Title = "Винни-Пух", Author = "Алан Милн", Price = 320, CoverImageUrl = "https://upload.wikimedia.org/wikipedia/en/1/10/WinniePooh.png", CategoryId = 5, IsPopular = false },
                new Book { Id = 22, Title = "Мэри Поппинс", Author = "Памела Трэверс", Price = 400, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780152058104-L.jpg", CategoryId = 5, IsPopular = false },
                new Book { Id = 23, Title = "Думай и богатей", Author = "Наполеон Хилл", Price = 480, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781585424337-L.jpg", CategoryId = 6, IsPopular = true },
                new Book { Id = 24, Title = "Богатый папа, бедный папа", Author = "Роберт Кийосаки", Price = 510, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781612680194-L.jpg", CategoryId = 6, IsPopular = true },
                new Book { Id = 25, Title = "7 навыков высокоэффективных людей", Author = "Стивен Кови", Price = 590, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780743269513-L.jpg", CategoryId = 6, IsPopular = false },
                new Book { Id = 26, Title = "Краткая история времени", Author = "Стивен Хокинг", Price = 620, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780553380163-L.jpg", CategoryId = 8, IsPopular = true },
                new Book { Id = 27, Title = "Sapiens", Author = "Юваль Ной Харари", Price = 650, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780062316097-L.jpg", CategoryId = 7, IsPopular = true },
                new Book { Id = 28, Title = "Homo Deus", Author = "Юваль Ной Харари", Price = 630, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780062464316-L.jpg", CategoryId = 7, IsPopular = false },
                new Book { Id = 29, Title = "Психология влияния", Author = "Роберт Чалдини", Price = 550, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780061241895-L.jpg", CategoryId = 9, IsPopular = true },
                new Book { Id = 30, Title = "Думай медленно, решай быстро", Author = "Даниэль Канеман", Price = 580, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780374533557-L.jpg", CategoryId = 9, IsPopular = false },
                new Book { Id = 31, Title = "Евгений Онегин", Author = "Александр Пушкин", Price = 390, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780192840196-L.jpg", CategoryId = 10, IsPopular = false },
                new Book { Id = 32, Title = "Мёртвые души", Author = "Николай Гоголь", Price = 460, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780300060218-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 33, Title = "Идиот", Author = "Фёдор Достоевский", Price = 590, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780375702242-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 34, Title = "Братья Карамазовы", Author = "Фёдор Достоевский", Price = 710, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780374528379-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 35, Title = "Пикник на обочине", Author = "Аркадий и Борис Стругацкие", Price = 470, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781613743416-L.jpg", CategoryId = 3, IsPopular = false },
                new Book { Id = 36, Title = "Солярис", Author = "Станислав Лем", Price = 500, CoverImageUrl = "https://upload.wikimedia.org/wikipedia/en/0/0e/Solaris_cover.jpg", CategoryId = 3, IsPopular = false },
                new Book { Id = 37, Title = "Над пропастью во ржи", Author = "Дж. Д. Сэлинджер", Price = 430, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780316769174-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 38, Title = "Великий Гэтсби", Author = "Фрэнсис Скотт Фицджеральд", Price = 480, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780743273565-L.jpg", CategoryId = 1, IsPopular = false },
                new Book { Id = 39, Title = "О дивный новый мир", Author = "Олдос Хаксли", Price = 520, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9780060850524-L.jpg", CategoryId = 3, IsPopular = false },
                new Book { Id = 40, Title = "451 градус по Фаренгейту", Author = "Рэй Брэдбери", Price = 490, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/9781451673319-L.jpg", CategoryId = 3, IsPopular = false }
            );
        }
    }
}