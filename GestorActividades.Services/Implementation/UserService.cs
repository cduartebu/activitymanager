using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Implementation;
using GestorActividades.Services.Validation;
using System;
using System.Collections.Generic;
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
                response.StatusMessage = validation.StatusMessage.Trim();
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<User>();

                User.UserName = User.UserName.Trim();
                User.FirstName = User.FirstName.Trim();
                User.LastName = User.LastName.Trim();
                User.EmailAddress = User.EmailAddress.Trim();
                User.Password = AuthService.CreateHash(User.Password);
                if (repository.GetAll().Any(x => x.UserName == User.UserName))
                {
                    response.StatusMessage = "The UserName already exists in the system.";
                    return response;
                }

                var team = new TeamService().AddTeam(new Team
                {
                    ProjectId = 2,
                    TeamName = User.UserName
                });

                User.TeamId = team.Data.TeamId;
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
                else
                {
                    response.Data.Password = string.Empty;
                }

                return response;
            }
        }

        public ResponseDto<IEnumerable<User>> GetUsers(string username)
        {
            var response = new ResponseDto<IEnumerable<User>>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<User>();

                response.Data = repo.Where(x => x.UserName != username).ToList().Select(x => new User
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailAddress = x.EmailAddress,
                    CreatedDt = x.CreatedDt
                });

                return response;
            }
        }

        public ResponseDto<User> UpdateUser(User User)
        {
            var response = new ResponseDto<User>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<User>();
                var repositoryTeam = unitOfWork.GetGenericRepository<Team>();
                IQueryable<User> users = repo.GetAll();

                response.Data = users.FirstOrDefault(x => x.UserId == User.UserId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(User);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage.Trim();
                    return response;
                }

                User.UserName = User.UserName.Trim();
                User.FirstName = User.FirstName.Trim();
                User.LastName = User.LastName.Trim();
                User.EmailAddress = User.EmailAddress.Trim();

                if (users.Any(x => x.UserName == User.UserName) && User.UserName != response.Data.UserName)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The UserName already exists in the system.";
                    return response;
                }

                if (User.TeamId != null && !repositoryTeam.GetAll().Any(x => x.TeamId == User.TeamId))
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The Team doesn't exist in the system.";
                    return response;
                }

                response.Data.UserId = User.UserId;
                response.Data.UserName = User.UserName;
                response.Data.FirstName = User.FirstName;
                response.Data.LastName = User.LastName;
                response.Data.EmailAddress = User.EmailAddress;
                response.Data.TeamId = User.TeamId;

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
                repo.Save();

                return response;
            }
        }
    }
}
