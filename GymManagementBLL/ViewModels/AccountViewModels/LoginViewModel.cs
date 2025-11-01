using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
