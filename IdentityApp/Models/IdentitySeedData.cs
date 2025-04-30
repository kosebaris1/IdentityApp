﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "admin";

        private const string adminPassword = "Admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);
            
            if (user == null)
            {
                user = new AppUser
                {
                    FullName = "Barış Köse",
                    UserName = adminUser,
                    Email = "kosebaris462@gmail.com",
                    PhoneNumber = "44444444"
                };

                await userManager.CreateAsync(user,adminPassword);
            }
        }

    }
}
