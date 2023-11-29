using Common.Entities;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Default"),x=>x.MigrationsAssembly(nameof(DataAccess))); 
});

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller}/{action}/{id?}"
    );

app.Run();
