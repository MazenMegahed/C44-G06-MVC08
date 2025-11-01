using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMembershipService
    {
         IEnumerable<MembershipViewModel> GetAllMemberships();
        bool CreateMembership(CreateMembershipViewModel input);
    }
}
