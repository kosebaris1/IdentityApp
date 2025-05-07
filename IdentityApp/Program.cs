using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IEmailSender,SmtpEmailSender>(i => 
            new SmtpEmailSender(
                builder.Configuration["EmailSender:Host"],
                builder.Configuration.GetValue<int>("EmailSender:Port"),
                builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                builder.Configuration["EmailSender:Username"],
                builder.Configuration["EmailSender:Password"])

                );

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<IdentityContext>(
                  options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
             );

            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = true;
            //  options.User.AllowedUserNameCharacters = "absjcksdv";

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.SignIn.RequireConfirmedEmail = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // zaten default ayarý bu ben göstermek için ekledim.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            IdentitySeedData.IdentityTestUser(app);

            app.Run();
        }
    }
}
