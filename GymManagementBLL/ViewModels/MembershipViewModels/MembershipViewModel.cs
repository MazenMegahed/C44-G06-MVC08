using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels
{
    public class MembershipViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                    return "Active";
                else
                    return "Expired";
            }
        }

        public int MemberId { get; set; }

        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public string MemberName { get; set; } = null!;

    }
}
