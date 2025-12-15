using GudangWebApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Konfigurasi Database (EF Core SQLite) ---
// Ini menghubungkan aplikasi ke file database "gudang.db"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=gudang.db"));

// --- 2. Konfigurasi MVC & Validasi Client-side ---
// Mengaktifkan fitur Controller+View sekaligus validasi otomatis di browser
builder.Services.AddControllersWithViews()
    .AddViewOptions(options => {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });

var app = builder.Build();

// --- 3. Konfigurasi Pipeline HTTP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // HSTS digunakan untuk keamanan di production (default 30 hari)
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Pola routing default: Controller=Home, Action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();