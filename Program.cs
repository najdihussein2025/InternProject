using Microsoft.EntityFrameworkCore;
using InternSystemProject.Data;
using InternSystemProject.Interfaces.Repositories;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Repositories;
using InternSystemProject.Services;
using InternSystemProject.Helpers;

var builder = WebApplication.CreateBuilder(args);

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// DATABASE
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// SESSION (for login/auth)
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// REPOSITORIES
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// SERVICES
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<IUserService, UserService>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// HELPERS
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<SessionHelper>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// MVC
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// MIDDLEWARE PIPELINE
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// session must be BEFORE MapControllerRoute
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();