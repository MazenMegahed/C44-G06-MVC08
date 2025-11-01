using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _PlanService;
    
        public PlanController(IPlanService planservice)
        {
            _PlanService = planservice;
        }
        public IActionResult Index()
        {
            var planss = _PlanService.GetAllPlans();
            return View(planss);
        }
        public IActionResult Details(int Id)
        {
            var plansDetails = _PlanService.GetPlanById(Id);
            return View(plansDetails);
        }
        public IActionResult Edit([FromRoute]int Id)
        {
            var updateplan = _PlanService.GetPlanToUpdate(Id);
            return View(updateplan);
        }
        [HttpPost]
        public IActionResult Edit(int Id, UpdatePlanViewModel UPVM)
        {
            var result = _PlanService.UpdatePlan(Id, UPVM);
            if (ModelState.IsValid)
            {
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return View(UPVM);
            }
           else { return RedirectToAction(nameof(Index)); }
        }
       
        [HttpPost]
        public IActionResult activate([FromRoute] int Id)
        {
            var result = _PlanService.Activate(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
