using GymManagmentDAL.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [RegularExpression(@"^[A-Za-z's]+$", ErrorMessage = "Name must contain only letters and single spaces between words")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be a Valid Egyptian Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Building Number must be a positive number")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Photo { get; set; } = null!;




        public HealthRecordViewModel HealthRecord {  get; set; }
    }

}


