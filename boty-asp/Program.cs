using Microsoft.AspNetCore.Authentication.Cookies;

namespace boty_asp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                // If a user tries to access a restricted page, send them here
                options.LoginPath = "/Account/Login";
                // Optional: Expire session after 60 minutes
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
            });

        builder.Services.AddAuthorization(options => {
            options.AddPolicy("MustBeAdmin", policy => 
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "2"));
            
            options.AddPolicy("MustBeSuperUser", policy => 
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "3"));

            options.AddPolicy("AdminAccess", policy =>
                policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "2", "3"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapStaticAssets();
        
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
        
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}