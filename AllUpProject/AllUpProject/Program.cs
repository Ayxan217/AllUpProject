using AllUpProject.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute("admin", "{area:exists}/{controller=home}/{action=index}/{id?}");

app.MapControllerRoute(
    "default", 
    "{controller=home}/{action=index}/{id?}");


app.Run();
