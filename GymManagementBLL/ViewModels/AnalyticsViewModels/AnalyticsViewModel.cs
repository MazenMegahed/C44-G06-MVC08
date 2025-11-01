using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels
{
    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int UpcomingSessions { get; set; }
        public int OngoingSessions { get; set; }
        public int CompletedSessions { get; set; }

        // Optional: Calculated properties
        public double MembershipUtilizationRate
        {
            get { return TotalMembers > 0 ? (double)ActiveMembers / TotalMembers * 100 : 0; }
        }

        public int TotalSessions
        {
            get { return UpcomingSessions + OngoingSessions + CompletedSessions; }
        }
    }
}
