using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Validation;
using System;
using System.Linq;

namespace GestorActividades.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public ResponseDto<User> AddUser(User User)
        {
            var response = new ResponseDto<User> { StatusCode = StatusCode.Error };

            var validation = ModelHelperValidator.ValidateComponentModel(User);

            if (!validation.Data)
            {
                response.StatusCode = StatusCode.Error;
                response.StatusMessage = validation.StatusMessage;
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<User>();

                User.UserId = User.UserId;
                User.EmailAddress = User.EmailAddress.Trim();

                if (repository.GetAll().Any(x => x.UserId == User.UserId))
                {
                    response.StatusMessage = "The User Email already exits in the system.";
                    return response;
                }

                User.CreatedDt = DateTime.Now;

                repository.InsertAndSave(User);


                response.StatusCode = StatusCode.Successful;
                response.Data = User;
            }

            return response;

        }

        public ResponseDto<User> GetUserById(int id)
        {
            var response = new ResponseDto<User>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<User>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.UserId == id);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                return response;
            }
        }

        public ResponseDto<User> UpdateUser(User User)
        {
            var response = new ResponseDto<User>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<User>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.UserId == User.UserId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(User);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage;
                    return response;
                }

                if (repo.GetAll().Any(x => x.UserId == User.UserId) && User.UserId != response.Data.UserId)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The User Email already exits in the system.";
                    return response;
                }

                response.Data.UserId = User.UserId;
                response.Data.EmailAddress = User.EmailAddress;
              
               

                repo.UpdateAndSave(response.Data);

                return response;
            }
        }

        public ResponseDto<bool> DeleteUser(int id)
        {
            var response = new ResponseDto<bool>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<User>();

                var User = repo.GetAll().FirstOrDefault(x => x.UserId == id);

                if (User == null)
                {
                    response.StatusCode = StatusCode.Error;
                    return response;
                }

                repo.Delete(User);

                return response;
            }
        }
    }
}
