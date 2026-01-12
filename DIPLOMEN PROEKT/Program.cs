using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- ВАЖНО: ТУК РЕГИСТРИРАМЕ БАЗАТА ДАННИ ---
// Това казва на приложението да използва SQLite и файла pc_configurator.db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=pc_configurator.db"));
// --------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Configurator}/{action=Index}/{id?}"); // Променихме Home на Configurator, за да отваря директно конфигуратора

app.Run();