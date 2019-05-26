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
                response.StatusMessage = validation.StatusMessage;
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<Activity>();

                Activity.ActivityId = Activity.ActivityId;
                Activity.Description = Activity.Description.Trim();

                if (repository.GetAll().Any(x => x.ActivityId == Activity.ActivityId))
                {
                    response.StatusMessage = "The Activity name already exits in the system.";
                    return response;
                }

                Activity.CreatedDt = DateTime.Now;

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

                response.Data = repo.GetAll().FirstOrDefault(x => x.ActivityId == Activity.ActivityId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(Activity);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage;
                    return response;
                }

                if (repo.GetAll().Any(x => x.ActivityId == Activity.ActivityId) && Activity.ActivityId != response.Data.ActivityId)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The Activity name already exits in the system.";
                    return response;
                }

                response.Data.ActivityId = Activity.ActivityId;
                response.Data.Description = Activity.Description;
                response.Data.StartDate = Activity.StartDate;
               

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

                return response;
            }
        }
    }
}
