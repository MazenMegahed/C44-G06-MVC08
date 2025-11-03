using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult Details([FromRoute]int id)
        {
            var member = _memberService.GetMemberDetails(id);
            if (member == null) RedirectToAction(nameof(Index)); 
            return View(member); 
        }


        public IActionResult HealthRecordDetails([FromRoute] int id)
        {
            var member = _memberService.GetMemberHealthRecord(id);
            return View(member);
        }

        public IActionResult MemberEdit([FromRoute] int id)
        {
            var member = _memberService.GetMemberDetails(id);
            return View(member);
        }
        [HttpPost]
        public IActionResult MemberEdit(MemberViewModel MV)
        {
            var member = _memberService.GetMemberDetails(MV.Id);
            if (member != null)
            {
                var memberToUpdate = new MemberToUpdateViewModel
                {
                    
                    Name = member.Name,
                    Photo = member.Photo,

                    Email = MV.Email,
                    Street = MV.StreetName,
                    City = MV.CityName,
                    Phone = MV.Phone,
                    BuildingNumber = MV.BuildingNumber,
                  

                };
                bool result = _memberService.UpdateMemberDetails(MV.Id, memberToUpdate);
                if (result) TempData["Success Message"] = "Member Updated Successfuly";
                else TempData["Failure Message"] = "Member failed to be Updated ";
            }
           

            return RedirectToAction(nameof(Index));
        }
        public IActionResult MemberCreate()
        {

          return View();
        }
        [HttpPost]
        public IActionResult MemberCreate(CreateMemberViewModel input)
        {
            if(ModelState.IsValid) {
              
                bool result = _memberService.CreateMember(input);
                if (result)
                { TempData["Success Message"] = "Member Created Successfuly";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Failure Message"] = "Member failed to be Created ";
                    return View(nameof(MemberCreate));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
               
            

           
        }

        public IActionResult Remove([FromRoute] int id)
        {
            bool result =_memberService.RemoveMember(id);
            if(result) return RedirectToAction(nameof(Index));
            else return View();
        }

    }
}
