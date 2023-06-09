using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using tysjyfgjkhfghjetsrstr.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();  
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.Use((context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;
        var routeValues = context.Request.RouteValues;
        // Mo¿esz tutaj wyœwietliæ informacje o dopasowanych trasach i wartoœciach parametrów
        Console.WriteLine($"Dopasowano trasê: {routePattern}");
        Console.WriteLine("Wartoœci parametrów:");
        foreach (var kvp in routeValues)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
    return next();
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "Wyniki",
       pattern: "Home/Wyniki",
       defaults: new { controller = "Home", action = "Wyniki" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
});
app.MapRazorPages();

app.Run();
