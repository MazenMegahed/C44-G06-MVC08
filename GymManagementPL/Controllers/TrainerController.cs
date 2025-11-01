using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var trainers =_trainerService.GetAllTrainers();
            return View(trainers);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTrainerViewModel input)
        {
            if (ModelState.IsValid)
            {
                bool result = _trainerService.CreateTrainer(input);
                if (result)
                {
                    TempData["Success Message"] = "Member Created Successfuly";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Failure Message"] = "Member failed to be Created ";
                    return View(nameof(Create));
                }

            }
            else
            {
                return View(nameof(Create));
            }
         
        }

        public IActionResult Details(int Id)
        {
            var trainerdetails=_trainerService.GetTrainerDetails(Id);
            return View(trainerdetails);
        }

        public IActionResult Remove([FromRoute] int Id)
        {
            ViewBag.trainerId=Id;
            return View();
        }
     
        public IActionResult RemoveConfirmed([FromForm]int Id)
        {
            var result= _trainerService.RemoveTrainer(Id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit([FromRoute] int Id)
        {
            var trainer = _trainerService.GetTrainerDetails(Id);
            return View(trainer);
        }
        [HttpPost]
        public IActionResult Edit(int Id, TrainerToUpdateViewModel input)
        {
            if (ModelState.IsValid)
            {
                var result  = _trainerService.UpdateTrainerDetails(Id, input);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return View();
            }
            else return View();


        }

    }
}
