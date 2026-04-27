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
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IStudentTaskRepository, StudentTaskRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStudentProjectRepository, StudentProjectRepository>();
builder.Services.AddScoped<IFinalProjectRepository, FinalProjectRepository>();
builder.Services.AddScoped<IStudentFinalProjectRepository, StudentFinalProjectRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// SERVICES
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IStudentTaskService, StudentTaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStudentProjectService, StudentProjectService>();
builder.Services.AddScoped<IFinalProjectService, FinalProjectService>();
builder.Services.AddScoped<IStudentFinalProjectService, StudentFinalProjectService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// HELPERS
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<SessionHelper>();
builder.Services.AddScoped<NotificationHelper>();

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