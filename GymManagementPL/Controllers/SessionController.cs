using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _SessionService;

        private readonly ITrainerService _trainerService;
        private readonly IUnitOfWork _unitOfWork;
        public SessionController(ISessionService sessionservice,ITrainerService trainerService, IUnitOfWork unitofwork)
        {
            _SessionService = sessionservice;
            _trainerService = trainerService;
            _unitOfWork = unitofwork;
        }
        public IActionResult Index()
        {
            var sessions =_SessionService.GetAllSessions();
            return View(sessions);
        }
        public IActionResult Details([FromRoute]int Id)
        {
            var sessiondetails = _SessionService.GetSessionById(Id);
            return View(sessiondetails);
        }
        public IActionResult Create()
        {
            ViewBag.Trainers = _trainerService.GetAllTrainers();

        var categoryRepo = _unitOfWork.GetRepository<Category>();
            var allCategories = categoryRepo.GetAll();

            ViewBag.Categories = allCategories?
                .Where(c => c != null);
           
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromForm]CreateSessionViewModel createSessionViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _SessionService.CreateSession(createSessionViewModel);

                    if (result) TempData["Success Message"] = "Session Created Successfuly";
                    else TempData["Failure Message"] = "Session failed to be Created ";
            }
           
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Remove([FromRoute] int Id)
        {
           var result = _SessionService.RemoveSession(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
