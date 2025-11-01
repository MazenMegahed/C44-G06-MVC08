using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel model);
        TrainerViewModel? GetTrainerDetails(int TrainerId);
        SessionViewModel? GetTrainerSessions(int TrainerId);
        bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel model);
        bool RemoveTrainer(int TrainerId);
    }
}
