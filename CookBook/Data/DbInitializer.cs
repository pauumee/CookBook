using CookBook.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            var roles = new List<RoleModel>
            {
                new RoleModel { Role = "Administrator", Description = "This role is for Administrator" },
                new RoleModel { Role = "Member", Description = "This role is for Members Only" }
            };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.Role) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role.Role, role.Description, DateTime.Now));
                }
            }

            var accounts = ParseJson<AccountModel>("user.json");

            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    var user = new ApplicationUser
                    {
                        UserName = account.Email,
                        Email = account.Email,
                        UserProfile = new User { FirstName = account.FirstName, LastName = account.LastName, UserName = account.Email }
                    };

                    // To ensure that the seed doesn't get repeated all the time, 
                    // by checking the database if the users on seed file 
                    // already exists or not.
                    if (await userManager.FindByNameAsync(account.Email) == null)
                    {
                        var result = await userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            await userManager.AddPasswordAsync(user, account.Password);
                            await userManager.AddToRoleAsync(user, account.Role);
                        }
                    }
                }
            }
        }

        private static T[] ParseJson<T>(string seedFile)
        {
            var json = Path.Combine("seed", seedFile);
            if (File.Exists(json))
            {
                return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(json)).ToArray();
            }
            return new T[0];
        }

        public class RoleModel
        {
            public string Role { get; set; }
            public string Description { get; set; }
        }

        public class AccountModel
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}
