using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// ← MIDDLEWARE: đọc cookie "Запомнить меня" → khôi phục session
app.Use(async (context, next) =>
{
    if (context.Session.GetInt32("UserId") == null)
    {
        var userIdCookie = context.Request.Cookies["RememberUserId"];
        var userNameCookie = context.Request.Cookies["RememberUserName"];

        if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int uid))
        {
            context.Session.SetInt32("UserId", uid);
            context.Session.SetString("UserName", userNameCookie ?? "");
        }
    }
    await next();
});

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// ← Phải migrate TRƯỚC app.Run(), vì app.Run() chặn luồng và không bao giờ return
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Tự động tạo bảng và cập nhật database lên hosting
}

app.Run();