using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Pharam_System___V6.Models;
using Pharam_System___V6.Repositry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }

).AddDefaultTokenProviders().AddEntityFrameworkStores<AppsDbContext>();
builder.Services.AddScoped<IRepositry<Category>, GeneraicRepositry<Category>>();
builder.Services.AddScoped<IRepositry<Product>, GeneraicRepositry<Product>>();
builder.Services.AddDbContext<AppsDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserTwoFactorTokenProvider<IdentityUser>, EmailTokenProvider<IdentityUser>>();
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
