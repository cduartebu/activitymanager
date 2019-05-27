using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Validation;
using System;
using System.Linq;

namespace GestorActividades.Services
{
    public class ActivityService : ServiceBase, IActivityService
    {
        public ResponseDto<Activity> AddActivity(Activity Activity)
        {
            var response = new ResponseDto<Activity> { StatusCode = StatusCode.Error };

            var validation = ModelHelperValidator.ValidateComponentModel(Activity);

            if (!validation.Data)
            {
                response.StatusCode = StatusCode.Error;
                response.StatusMessage = validation.StatusMessage.Trim();
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<Activity>();
                var repositoryTeam = unitOfWork.GetGenericRepository<Team>();

                Activity.Description = Activity.Description.Trim();                
                Activity.CreatedDt = DateTime.Now;

                if (!repositoryTeam.GetAll().Any(x => x.TeamId == Activity.TeamId))
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The Team doesn't exist in the system.";
                    return response;
                }

                repository.InsertAndSave(Activity);


                response.StatusCode = StatusCode.Successful;
                response.Data = Activity;
            }

            return response;

        }

        public ResponseDto<Activity> GetActivityById(int id)
        {
            var response = new ResponseDto<Activity>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Activity>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.ActivityId == id);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                return response;
            }
        }

        public ResponseDto<Activity> UpdateActivity(Activity Activity)
        {
            var response = new ResponseDto<Activity>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Activity>();
                var repositoryTeam = unitOfWork.GetGenericRepository<Team>();

                IQueryable<Activity> activities = repo.GetAll();

                response.Data = activities.FirstOrDefault(x => x.ActivityId == Activity.ActivityId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(Activity);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage.Trim();
                    return response;
                }

                if (!repositoryTeam.GetAll().Any(x => x.TeamId == Activity.TeamId))
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The team doesn't exist in the system.";
                    return response;
                }

                response.Data.ActivityId = Activity.ActivityId;
                response.Data.Description = Activity.Description;
                response.Data.StartDate = Activity.StartDate;
                response.Data.DueDate = Activity.DueDate;

                repo.UpdateAndSave(response.Data);

                return response;
            }
        }

        public ResponseDto<bool> DeleteActivity(int id)
        {
            var response = new ResponseDto<bool>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Activity>();

                var Activity = repo.GetAll().FirstOrDefault(x => x.ActivityId == id);

                if (Activity == null)
                {
                    response.StatusCode = StatusCode.Error;
                    return response;
                }

                repo.Delete(Activity);
                repo.Save();

                return response;
            }
        }
    }
}
