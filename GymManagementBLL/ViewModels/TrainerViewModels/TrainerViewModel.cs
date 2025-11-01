using GymManagmentDAL.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels
{
    public class TrainerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Speciality { get; set; } = null!;
        public string? HiringStartDate { get; set; }
        public string? CurrentSessionStartDate { get; set; }
        public string? CurrentSessionEndDate { get; set; }
        public string CityName { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int BuildingNumber { get; set; }

    }

}
