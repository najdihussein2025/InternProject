using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternSystemProject.Data;
using InternSystemProject.Interfaces.Repositories;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Repositories;
using InternSystemProject.Services;

var builder = WebApplication.CreateBuilder(args);

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// DATABASE
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddHttpContextAccessor();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// REPOSITORIES
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// SERVICES
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
// AUTHENTICATION / AUTHORIZATION
// ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection["Key"] ?? throw new InvalidOperationException("JWT Key is missing.");
var issuer = jwtSection["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is missing.");
var audience = jwtSection["Audience"] ?? throw new InvalidOperationException("JWT Audience is missing.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue("jwt", out var token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();