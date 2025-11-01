using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateMembership(CreateMembershipViewModel input)
        {
            if (!IsMemberExist(input.MemberId))
                return false;
            if (!IsPlanExist(input.PlanId))
                return false;
            if (!IsValidDateRange(input.StartDate, input.EndDate))
                return false;

            var membership = new Membership
            {
                MemberId = input.MemberId,
                PlanId = input.PlanId,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                //CreatedAt = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow,
          
            };



            _unitOfWork.GetRepository<Membership>().Add(membership);

            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MembershipViewModel> GetAllMemberships()
        {
            var memberships = _unitOfWork.GetRepository<Membership>().GetAll()
               .OrderByDescending(x => x.CreatedAt);

            if (memberships == null || !memberships.Any())
                return [];

            var mappedMemberships = _mapper.Map<IEnumerable<Membership>, IEnumerable<MembershipViewModel>>((IEnumerable<Membership>)memberships);
            foreach (var membership in mappedMemberships)
            {
                membership.MemberName= _unitOfWork.GetRepository<Member>().GetById(membership.MemberId).Name;
                membership.PlanName = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId).Name;
            }

            return mappedMemberships;
        }

        #region Healper Methods

        private bool IsMemberExist(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            return member is null ? false : true;
        }
        private bool IsPlanExist(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            return plan is null ? false : true;
        }

        private bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate /*&& startDate > DateTime.UtcNow*/;
        }
        #endregion
    }
}
