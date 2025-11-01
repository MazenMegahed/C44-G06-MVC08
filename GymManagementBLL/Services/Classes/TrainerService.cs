using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels; 
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerService :ITrainerService
    {


        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel model)
        {
            try
            {
                if (IsEmailExists(model.Email))
                    return false;

                if (IsPhoneExists(model.Phone))
                    return false;

                var trainer = new Trainer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Address = new Address
                    {
                        BuildingNumber = model.BuildingNumber,
                        City = model.City,
                        Street = model.Street,
                    },
                    Specialities=model.Speciality
                  
                };

                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];

            if (trainers is null || !trainers.Any())
                return [];

            var trainerViewModels = trainers.Select(x => new TrainerViewModel
            {
                Id = x.Id,
           
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth.ToShortDateString(),
                Gender = x.Gender.ToString(),
                Address = FormatAddress(x.Address),
                Speciality=x.Specialities.ToString()
                ,HiringStartDate =x.CreatedAt.ToShortDateString(),
            }).ToList();

            return trainerViewModels;
        }
        public bool UpdateTrainerDetails(int trainerId, TrainerToUpdateViewModel model)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer is null)
                return false;

            if (IsEmailExists(model.Email))
                return false;

            if (IsPhoneExists(model.Phone))
                return false;

            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.City = model.CityName;
            trainer.Address.Street = model.StreetName;
            trainer.UpdatedAt = DateTime.Now;
            trainer.Specialities = model.Speciality;
            trainer.Gender = model.Gender;
            _unitOfWork.GetRepository<Trainer>().Update(trainer);
            _unitOfWork.SaveChanges();
            return true;
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            if (trainer == null)
                return null;

            var trainerViewModel = new TrainerViewModel
            {
                Id = trainer.Id,
         
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                Gender = trainer.Gender.ToString(),
                Address = FormatAddress(trainer.Address)
            };

            var activetrainersession = _unitOfWork.GetRepository<Session>()
               .GetAll(x => x.TrainerId == trainerId && x.StartDate < DateTime.UtcNow&& x.EndDate> DateTime.UtcNow)
                 .FirstOrDefault();
            if (activetrainersession != null)
            {
             trainerViewModel.CurrentSessionStartDate=activetrainersession.StartDate.ToString();
                trainerViewModel.CurrentSessionEndDate = activetrainersession.EndDate.ToString();
            }

            return trainerViewModel;

        }


        public bool RemoveTrainer(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);

            if (trainer is null)
                return false;


            var activesessions = _unitOfWork.GetRepository<Session>().GetAll(x => x.TrainerId == TrainerId && x.StartDate > DateTime.UtcNow);
            if (activesessions.Any())
                return false;

            try
            {
                if (activesessions.Any())
                {
                    foreach (var sessionn in activesessions)
                        _unitOfWork.GetRepository<Session>().Delete(sessionn);
                }

                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SessionViewModel? GetTrainerSessions(int TrainerId)
        {
            throw new NotImplementedException();
        }






        #region Helper Methods
        private string FormatAddress(Address address)
        {
            if (address is null)
                return "N/A";
            return $"{address.BuildingNumber}, {address.Street}, {address.City}";
        }

        private bool IsEmailExists(string email, int excludeTrainerId = 0)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(m => m.Email == email && m.Id != excludeTrainerId);
        }

        private bool IsPhoneExists(string phone, int excludeTrainerId = 0)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(m => m.Phone == phone && m.Id != excludeTrainerId);
        }






        #endregion
    }
}
