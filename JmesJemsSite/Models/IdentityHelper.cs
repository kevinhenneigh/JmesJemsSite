using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public static class IdentityHelper
    {
        // Role names
        public const string Administrator = "Administrator";
        public const string Customer = "Customer";

        public static void SetIdentityOptions(IdentityOptions options)
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Set password strength
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;

            // Set password lockout time and failed attempts
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 6;
        }

        public static async Task CreateRoles(IServiceProvider provider, 
            params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = 
                provider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles if they do not exist
            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        internal static async Task CreateDefaultAdministrator(IServiceProvider serviceProvider)
        {
            const string email = "kevinhenneigh@hotmail.com";
            const string username = "administrator";
            const string password = "administrator";

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Check if any users in database
            if (userManager.Users.Count() == 0)
            {
                IdentityUser administrator = new IdentityUser()
                {
                    Email = email,
                    UserName = username
                };

                // Create Administrator
                await userManager.CreateAsync(administrator, password);

                // Add to administrator role
                await userManager.AddToRoleAsync(administrator, Administrator);
            }
        }
    }
}
