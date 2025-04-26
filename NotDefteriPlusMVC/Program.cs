using Microsoft.EntityFrameworkCore;
using NotDefteriPlusMVC.Abstracts.Repositories;
using NotDefteriPlusMVC.Abstracts.Services;
using NotDefteriPlusMVC.Data;
using NotDefteriPlusMVC.Models;
using NotDefteriPlusMVC.Repositories;
using NotDefteriPlusMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// dbcontext in dependency injection i�in service olarak eklenmesi
builder.Services.AddDbContext<NotDefteriDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotDefteriPlusConnection")));

// Identity i�in gerekli olan servislerin eklenmesi
builder.Services.AddDefaultIdentity<Kullanici>()
    .AddEntityFrameworkStores<NotDefteriDbContext>();

// yetkisiz bir butona bas�nca default olan Identity/Account/Login sayfas� yerine a�a��daki route a gitmesi i�in;
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// Repository lerin dependency injection i�in servislere eklenmesi
builder.Services.AddTransient<IBolumRepository, BolumRepository>();
builder.Services.AddTransient<IDersRepository, DersRepository>();
builder.Services.AddTransient<INotRepository, NotRepository>();
builder.Services.AddTransient<IBolumDersRepository, BolumDersRepository>();
builder.Services.AddTransient<IKullaniciBolumRepository, KullaniciBolumRepository>();
builder.Services.AddTransient<IFakulteRepository, FakulteRepository>();

// accountService lerin dependency injection i�in servislere eklenmesi
builder.Services.AddTransient<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
