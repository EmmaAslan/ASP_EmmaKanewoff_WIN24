using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddIdentity<UserEntity, IdentityRole>(x =>
    {
        x.SignIn.RequireConfirmedAccount = false;
        x.User.RequireUniqueEmail = true;
        x.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
    {
        x.LoginPath = "/auth/login";
        x.SlidingExpiration = true;
        x.ExpireTimeSpan = TimeSpan.FromHours(1);
        x.SlidingExpiration = true;
        x.Cookie.HttpOnly = true;
    });


builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();



var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/projects"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope  = app.Services.CreateScope())
{
    await AppDbSeeder.SeedStatuses(scope.ServiceProvider);
}

app.Run();
