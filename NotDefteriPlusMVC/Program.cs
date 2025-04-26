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

// dbcontext in dependency injection için service olarak eklenmesi
builder.Services.AddDbContext<NotDefteriDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotDefteriPlusConnection")));

// Identity için gerekli olan servislerin eklenmesi
builder.Services.AddDefaultIdentity<Kullanici>()
    .AddEntityFrameworkStores<NotDefteriDbContext>();

// yetkisiz bir butona basýnca default olan Identity/Account/Login sayfasý yerine aþaðýdaki route a gitmesi için;
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// Repository lerin dependency injection için servislere eklenmesi
builder.Services.AddTransient<IBolumRepository, BolumRepository>();
builder.Services.AddTransient<IDersRepository, DersRepository>();
builder.Services.AddTransient<INotRepository, NotRepository>();
builder.Services.AddTransient<IBolumDersRepository, BolumDersRepository>();
builder.Services.AddTransient<IKullaniciBolumRepository, KullaniciBolumRepository>();
builder.Services.AddTransient<IFakulteRepository, FakulteRepository>();

// accountService lerin dependency injection için servislere eklenmesi
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
