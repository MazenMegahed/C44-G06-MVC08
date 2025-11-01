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

                // Seed super admin user
                var superAdmin = new ApplicationUser
                {
                    FirstName = "Mazen",
                    LastName = "Megahed",
                    UserName = "MazenMegahed",
                    Email = "test@gmail.com",
                    PhoneNumber = "01069000383"
                };

                userManager.CreateAsync(superAdmin, "P@123456").Wait();
                userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();

                // Seed admin user
                var admin = new ApplicationUser
                {
                    FirstName = "Mohamed",
                    LastName = "Ali",
                    UserName = "MohamedAli",
                    Email = "MohamedAli@gmail.com",
                    PhoneNumber = "523697411"
                };
                userManager.CreateAsync(admin, "P@ssword").Wait();
                userManager.AddToRoleAsync(admin, "Admin").Wait();

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
