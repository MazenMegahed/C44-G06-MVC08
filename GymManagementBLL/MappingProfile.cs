using AutoMapper;
using GymManagementBLL.ViewModels;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymManagementBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapSession();
        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
            CreateMap<MemberViewModel, MemberToUpdateViewModel>();
            CreateMap<TrainerViewModel, TrainerToUpdateViewModel>();
            CreateMap<Membership, MembershipViewModel>().ReverseMap();
            CreateMap<CreateMembershipViewModel, MembershipViewModel>();
        }
    }

}
