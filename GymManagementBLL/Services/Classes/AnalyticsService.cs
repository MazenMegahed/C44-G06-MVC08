using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         
        }

        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessionRepository = _unitOfWork.GetRepository<Session>();
            var currentTime = DateTime.Now;

            return new AnalyticsViewModel
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = sessionRepository.GetAll(x => x.StartDate > currentTime).Count(),
                OngoingSessions = sessionRepository.GetAll(x => x.StartDate <= currentTime && x.EndDate >= currentTime).Count(),
                CompletedSessions = sessionRepository.GetAll(x => x.EndDate < currentTime).Count()
            };
        }



    }
}