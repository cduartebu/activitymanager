using GestorActividades.Infrastructure;
using GestorActividades.Infrastructure.Models;
using GestorActividades.Services.Validation;
using System;
using System.Linq;

namespace GestorActividades.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        public ResponseDto<Team> AddTeam(Team Team)
        {
            var response = new ResponseDto<Team> { StatusCode = StatusCode.Error };

            var validation = ModelHelperValidator.ValidateComponentModel(Team);

            if (!validation.Data)
            {
                response.StatusCode = StatusCode.Error;
                response.StatusMessage = validation.StatusMessage.Trim();
                return response;
            }

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repository = unitOfWork.GetGenericRepository<Team>();
                var repositoryProject= unitOfWork.GetGenericRepository<Project>();
                                
                Team.TeamName = Team.TeamName.Trim();

                if (!repositoryProject.GetAll().Any(x => x.ProjectId == Team.ProjectId))
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The project doesn't exist in the system.";
                    return response;
                }

                Team.CreatedDt = DateTime.Now;

                repository.InsertAndSave(Team);


                response.StatusCode = StatusCode.Successful;
                response.Data = Team;
            }

            return response;

        }

        public ResponseDto<Team> GetTeamById(int id)
        {
            var response = new ResponseDto<Team>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Team>();

                response.Data = repo.GetAll().FirstOrDefault(x => x.TeamId == id);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Error;
                }

                return response;
            }
        }

        public ResponseDto<Team> UpdateTeam(Team Team)
        {
            var response = new ResponseDto<Team>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Team>();
                var repositoryProject = unitOfWork.GetGenericRepository<Project>();
                IQueryable<Team> teams = repo.GetAll();

                response.Data = teams.FirstOrDefault(x => x.TeamId == Team.TeamId);

                if (response.Data == null)
                {
                    response.StatusCode = StatusCode.Warning;
                    return response;
                }

                var validation = ModelHelperValidator.ValidateComponentModel(Team);

                if (!validation.Data)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = validation.StatusMessage.Trim();
                    return response;
                }

                if (teams.Any(x => x.TeamName == Team.TeamName) && Team.TeamName != response.Data.TeamName)
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The team name already exists in the system.";
                    return response;
                }

                if (!repositoryProject.GetAll().Any(x => x.ProjectId == Team.ProjectId))
                {
                    response.StatusCode = StatusCode.Error;
                    response.StatusMessage = "The project doesn't exist in the system.";
                    return response;
                }

                response.Data.TeamId = Team.TeamId;
                response.Data.TeamName = Team.TeamName;
                response.Data.ProjectId = Team.ProjectId;


                repo.UpdateAndSave(response.Data);

                return response;
            }
        }

        public ResponseDto<bool> DeleteTeam(int id)
        {
            var response = new ResponseDto<bool>(StatusCode.Successful);

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                var repo = unitOfWork.GetGenericRepository<Team>();

                var Team = repo.GetAll().FirstOrDefault(x => x.TeamId == id);

                if (Team == null)
                {
                    response.StatusCode = StatusCode.Error;
                    return response;
                }

                repo.Delete(Team);
                repo.Save();

                return response;
            }
        }
    }
}
