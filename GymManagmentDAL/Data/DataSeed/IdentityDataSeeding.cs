using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class IdentityDataSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {

                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                {
                      new IdentityRole() { Name = "SuperAdmin" },
                      new IdentityRole() { Name = "Admin" }
                };

                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }
             
                if (!userManager.Users.Any())
                {
                    var superAdmin = new ApplicationUser
                    {
                        FirstName = "Mazen",
                        LastName = "Megahed",
                        UserName = "MazenMegahed",
                        Email = "mazen@gmail.com",
                        PhoneNumber = "1234567890"
                    };

                    var createResult = userManager.CreateAsync(superAdmin, "P@ssword123").Result;
                    userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();

                    var admin = new ApplicationUser
                    {
                        FirstName = "Mohamed",
                        LastName = "Ali",
                        UserName = "MohamedAli",
                        Email = "MohamedAli@gmail.com",
                        PhoneNumber = "523697411"
                    };

                    userManager.CreateAsync(admin, "P@ssword2").Wait();
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed: {ex}");
                return false;
            }
        }
    }
}
