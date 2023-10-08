using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AirsoftRUs.Data;
using AirsoftRUs.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AirsoftRUsContextConnection") ?? throw new InvalidOperationException("Connection string 'AirsoftRUsContextConnection' not found.");

builder.Services.AddDbContext<AirsoftRUsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AirsoftRUsUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AirsoftRUsContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
