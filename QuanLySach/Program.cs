using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

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
else
{
    app.UseExceptionHandler("/Home/Error");
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

// ← MIDDLEWARE: CHẾ ĐỘ BẢO TRÌ
// Khi admin tick "Bật chế độ bảo trì" trong QuanTri > Settings thì toàn bộ trang NGƯỜI DÙNG
// sẽ trả về trang bảo trì (HTTP 503). Những thứ sau vẫn hoạt động bình thường:
//   - Mọi trang /QuanTri/... (trang admin)
//   - Trang đăng nhập /Account/Login, /Account/Logout và POST về "/" (form đăng nhập) để admin vẫn đăng nhập được
//   - Admin đã đăng nhập (có session AdminId) vẫn xem được trang người dùng để kiểm tra
//   - File tĩnh (css/js/images) đã được UseStaticFiles xử lý ở phía trên
app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    bool isAllowedPath =
        path.StartsWithSegments("/QuanTri", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/Account/Login", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/Account/Logout", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWithSegments("/Home/Error", StringComparison.OrdinalIgnoreCase) || // để UseExceptionHandler hiện đúng trang lỗi, không bị nhầm thành trang bảo trì
                                                                                      // Form đăng nhập POST về "/" (vì route mặc định là Account/Login nên asp-action="Login" sinh ra URL "/").
                                                                                      // Chỉ cho phép POST; còn GET "/" vẫn hiện trang bảo trì cho khách.
        (HttpMethods.IsPost(context.Request.Method) && (path.Value == "/" || string.IsNullOrEmpty(path.Value)));

    bool isAdminLoggedIn = context.Session.GetInt32("AdminId") != null;

    if (isAllowedPath || isAdminLoggedIn)
    {
        await next();
        return;
    }

    SiteSetting? settings = null;
    try
    {
        var db = context.RequestServices.GetRequiredService<AppDbContext>();
        settings = await db.SiteSettings.AsNoTracking().FirstOrDefaultAsync();
    }
    catch
    {
        // Không đọc được DB thì cho website chạy bình thường, tránh làm sập cả site.
    }

    if (settings == null || !settings.MaintenanceMode)
    {
        await next();
        return;
    }

    var message = string.IsNullOrWhiteSpace(settings.MaintenanceMessage)
        ? "Trang web đang bảo trì, vui lòng quay lại sau."
        : settings.MaintenanceMessage;
    var safeMessage = System.Net.WebUtility.HtmlEncode(message)
        .Replace("\r\n", "<br>").Replace("\n", "<br>");
    var safeStoreName = System.Net.WebUtility.HtmlEncode(settings.StoreName ?? "");

    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
    context.Response.Headers["Retry-After"] = "3600";
    context.Response.Headers["Cache-Control"] = "no-store";
    context.Response.ContentType = "text/html; charset=utf-8";

    var html = $$"""
    <!DOCTYPE html>
    <html lang="ru">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>{{safeStoreName}} — Технические работы</title>
        <style>
            * { box-sizing: border-box; }
            body {
                margin: 0; min-height: 100vh; display: flex; align-items: center; justify-content: center;
                background: #2c1a0e; font-family: 'Lato', Arial, sans-serif; color: #2c1a0e; padding: 20px;
            }
            .card {
                background: #fdf6e3; max-width: 560px; width: 100%; padding: 48px 36px; text-align: center;
                border-radius: 16px; border-top: 6px solid #b8860b; box-shadow: 0 20px 50px rgba(0,0,0,.4);
            }
            .icon { font-size: 64px; margin-bottom: 12px; }
            .store { color: #8b6508; font-weight: 700; letter-spacing: 1px; margin-bottom: 8px; }
            h1 { margin: 0 0 6px; font-size: 26px; }
            h2 { margin: 0 0 24px; font-size: 16px; font-weight: 400; color: #555; }
            .msg { background: #f5e6c8; border-radius: 10px; padding: 16px 20px; line-height: 1.6; font-size: 16px; }
            .admin-btn {
                display: inline-block; margin-top: 24px; padding: 11px 26px; background: #b8860b; color: #fff;
                text-decoration: none; font-weight: 700; font-size: 15px; border-radius: 8px; transition: background .2s;
            }
            .admin-btn:hover { background: #8b6508; }
        </style>
    </head>
    <body>
        <div class="card">
            <div class="icon">🛠️</div>
            <div class="store">{{safeStoreName}}</div>
            <h1>Сайт на техническом обслуживании</h1>
            <h2>Trang web đang bảo trì</h2>
            <div class="msg">{{safeMessage}}</div>
            <a class="admin-btn" href="/Account/Login"> Вход администратора</a>
        </div>
    </body>
    </html>
    """;

    await context.Response.WriteAsync(html);
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