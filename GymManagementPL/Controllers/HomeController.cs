using GymManagementBLL.Services.Interfaces;
using GymManagementPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public IActionResult Index()
        {
            var analytics = _analyticsService.GetAnalyticsData();
            return View(analytics);
        }



        //[HttpGet("Index2)]

        //public IActionResult Index(int id)
        //{

        //    return View(id.ToString()); 
        //}
    }
}
