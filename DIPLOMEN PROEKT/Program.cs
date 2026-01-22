using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator; // Това е нужно, за да намери ExcelPriceService

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// 1. НАСТРОЙКИ НА УСЛУГИТЕ (SERVICES)
// =========================================================

// Важно за ExcelDataReader (за да чете правилно файла)
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

// Добавяме поддръжка за Контролери и Views (MVC)
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Настройка на Базата Данни (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрираме твоя Excel Сървис
builder.Services.AddScoped<ExcelPriceService>();

// =========================================================
// КРАЙ НА НАСТРОЙКИТЕ - БИЛДВАНЕ НА APP
// =========================================================
var app = builder.Build();

// 2. ОПЕРАЦИИ С БАЗАТА (MIGRATIONS)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Създава/Обновява базата автоматично
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// 3. HTTP REQUEST PIPELINE (MIDDLEWARE)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();