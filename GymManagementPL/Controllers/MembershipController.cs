using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IPlanService _planService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipController(IMembershipService membershipService,IPlanService planService, IUnitOfWork unitofwork)
        {
            _membershipService = membershipService;
            _planService = planService;
            _unitOfWork = unitofwork;
        }
        public IActionResult Index()
        {
            var memberships = _membershipService.GetAllMemberships();
            return View(memberships);
        }

        public IActionResult Create( )
        {
            ViewBag.Plans = _planService.GetAllPlans();

            var memberRepo = _unitOfWork.GetRepository<Member>();
            var allmembers = memberRepo.GetAll();

            ViewBag.Members = allmembers?
                .Where(c => c != null);

            return View();
          
        }

        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel newvm)
        {
            if (ModelState.IsValid)
            {
                var result = _membershipService.CreateMembership(newvm);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
