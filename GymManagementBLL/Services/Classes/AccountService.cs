using GymManagementBLL.ViewModels;
using GymManagementBLL.Services.Interfaces;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ApplicationUser? ValidateUser(LoginViewModel input)
        {
            var user = _userManager.FindByEmailAsync(input.Email).Result;
            if (user == null) return null;

            var isValidPassword = _userManager.CheckPasswordAsync(user, input.Password).Result;
            return isValidPassword ? user : null;
        }
    }
}