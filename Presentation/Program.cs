var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllersWithViews();

app.MapControllerRoute(
    name: "default",
    pattern:"{controller}/{action}/{id?}"
    );

app.Run();
