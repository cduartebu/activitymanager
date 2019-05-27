using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Validation;
using System;
using System.Linq;

namespace GestorActividades.Services
{
    public class DeliverableService : ServiceBase, IDeliverableService
    {
        public ResponseDto<Deliverable> AddDeliverable(Deliverable Deliverable)
        {
            var response = new ResponseDto<Deliverable> { StatusCode = StatusCode.Error };

            var validation = ModelHelperValidator.ValidateComponentModel(Deliverable);

            if (!validation.Data)
            {
                response.StatusCode = StatusCode.Error;
                response.StatusMessage = validation.StatusMessage.Trim();
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<Deliverable>();

                Deliverable.DeliverableId = Deliverable.DeliverableId;
                Deliverable.Description = Deliverable.Description.Trim();

                if (repository.GetAll().Any(x => x.DeliverableId == Deliverable.DeliverableId))
                {
                    response.StatusMessage = "The Deliverable name already exits in the system.";
                    return response;
                }

                Deliverable.CreatedDt = DateTime.Now;

                repository.InsertAndSave(Deliverable);


                response.StatusCode = StatusCode.Successful;
                response.Data = Deliverable;
            }

            return response;

        }

        public ResponseDto<Deliverable> GetDeliverableById(int id)
        {
            var response = new ResponseDto<Deliverable>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Deliverable>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.DeliverableId == id);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                return response;
            }
        }

        public ResponseDto<Deliverable> UpdateDeliverable(Deliverable Deliverable)
        {
            var response = new ResponseDto<Deliverable>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Deliverable>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.DeliverableId == Deliverable.DeliverableId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(Deliverable);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage.Trim();
                    return response;
                }

                if (repo.GetAll().Any(x => x.DeliverableId == Deliverable.DeliverableId) && Deliverable.DeliverableId != response.Data.DeliverableId)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The Deliverable name already exits in the system.";
                    return response;
                }

                response.Data.DeliverableId = Deliverable.DeliverableId;
                response.Data.Description = Deliverable.Description;
                response.Data.DueDate = Deliverable.DueDate;
               

                repo.UpdateAndSave(response.Data);

                return response;
            }
        }

        public ResponseDto<bool> DeleteDeliverable(int id)
        {
            var response = new ResponseDto<bool>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Deliverable>();

                var Deliverable = repo.GetAll().FirstOrDefault(x => x.DeliverableId == id);

                if (Deliverable == null)
                {
                    response.StatusCode = StatusCode.Error;
                    return response;
                }

                repo.Delete(Deliverable);
                repo.Save();

                return response;
            }
        }
    }
}
