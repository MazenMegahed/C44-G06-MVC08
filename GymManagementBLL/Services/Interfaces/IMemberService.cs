using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel model);
        MemberViewModel? GetMemberDetails(int memberId);
        HealthRecordViewModel? GetMemberHealthRecord(int memberId);
        bool UpdateMemberDetails(int memberId, MemberToUpdateViewModel model);
        bool RemoveMember(int memberId);
    }





}
