using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels
{


    public class CreateMembershipViewModel
    {
        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date & Time")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date & Time")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Member is required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Plan is required")]
        [Display(Name = "Plan")]
        public int PlanId { get; set; }

    }
}
