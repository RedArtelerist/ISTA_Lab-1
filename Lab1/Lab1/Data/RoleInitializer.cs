using Lab1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "creator@gmail.com";
            string password = "ms611001ms";
            if (await roleManager.FindByNameAsync("creator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("creator"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User creator = new User { Email = adminEmail, UserName = adminEmail, Nick = "red_artelerist"};
                IdentityResult result = await userManager.CreateAsync(creator, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(creator, "creator");
                }
            }
        }
    }
}
